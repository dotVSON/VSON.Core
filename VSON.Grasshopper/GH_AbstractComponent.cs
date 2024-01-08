using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Grasshopper;
using Grasshopper.Kernel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VSON.Core;

namespace VSON.Grasshopper
{
    public class GH_AbstractComponent : Component
    {
        #region Constructor

        private GH_AbstractComponent() : base() { }
            
        public GH_AbstractComponent(GH_AbstractDocument document, IGH_Component component) : this()
        {
            this.Message = component.Message;
            this.IsHidden = component.Hidden;
            this.IsLocked = component.Locked;
            this.IsSpecial = false;
            this.ComponentType = ComponentType.GrasshopperComponent;

            this.Initialize(document, component);
            this.PopulateParameters(component);
        }

        public GH_AbstractComponent(GH_AbstractDocument document, IGH_Param parameter) : this()
        {
            this.Message = string.Empty;
            this.IsHidden = false;
            this.IsLocked = false;
            this.IsSpecial = GH_AbstractParameter.IsStandaloneComponent(parameter);
            this.ComponentType = this.IsSpecial ? ComponentType.GrasshopperSpecialParam : ComponentType.GenericComponent;

            this.Initialize(document, parameter);
            this.PopulateParameters(parameter);
        }

        #endregion Constructor

        public Table<string, Type, object> AttributeTable { get; set; }

        #region Methods  
        private void Initialize(GH_AbstractDocument document, IGH_DocumentObject documentObject)
        {
            this.Discriminator = documentObject.GetType().FullName;
            this.ComponentGuid = documentObject.ComponentGuid;
            this.InstanceGuid = documentObject.InstanceGuid;
            this.Name = documentObject.Name;
            this.NickName = documentObject.NickName;
            this.Pivot = documentObject.Attributes.Pivot;
            this.Bounds = documentObject.Attributes.Bounds;

            // Test
            //this.PopulateAttributeTable(documentObject.Attributes);

            document.Register(this);
        }

        public Dictionary<string, object> GetAllProperties(object instanceObject)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            foreach (PropertyInfo propertyInfo in instanceObject.GetType().GetProperties())
            {
                if (propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsPublic)
                {
                    properties.Add(propertyInfo.Name, propertyInfo.GetValue(instanceObject));
                }
            }
            return properties;
        }

        public string SpawnComponent(IGH_DocumentObject docObject)
        {
            StringBuilder log = new StringBuilder();

            // Pack Component
            Dictionary<string, object> attributes = GetAllProperties(docObject.Attributes);
            string jsonAttributes = JsonConvert.SerializeObject(attributes, Formatting.Indented, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
            });

            JObject unpackedAttributes = JsonConvert.DeserializeObject(jsonAttributes) as JObject;
            Guid guid = new Guid("d93100b6-d50b-40b2-831a-814659dc38e3");
            IGH_DocumentObject component = Instances.ComponentServer.EmitObject(guid);
            if (component != null)
            {
                component.CreateAttributes();
                foreach (JProperty jProperty in unpackedAttributes.Properties())
                {
                    var name = jProperty.Name;
                    PropertyInfo propertyInfo = component.Attributes.GetType().GetProperty(name);
                    /*PropertyInfo propertyInfo = null;
                    var allProps = component.Attributes.GetType().GetProperties();
                    foreach (var p in component.Attributes.GetType().GetProperties())
                    {
                        if (p.Name == name)
                        {
                            propertyInfo = p;
                            break;
                        }
                    }*/
                    //PropertyInfo propertyInfo = component.GetType().GetProperty(name);
                    if (propertyInfo != null)
                    {
                        log.AppendLine(propertyInfo.GetValue(component.Attributes).ToString());

                        var value = jProperty.Value;
                        var vObj = JsonConvert.DeserializeObject(jProperty.ToString());
                        propertyInfo.SetValue(component.Attributes, jProperty.Value);
                        log.AppendLine(propertyInfo.GetValue(component.Attributes).ToString());
                    }
                    log.AppendLine("");
                }
            }

            return log.ToString();
        }


        public void PopulateAttributeTable(IGH_Attributes attributes)
        {
            this.AttributeTable = new Table<string, Type, object>();
            foreach (PropertyInfo property in attributes.GetType().GetProperties())
            {
                if (property.SetMethod != null && property.SetMethod.IsPublic)
                {
                    this.AttributeTable.Add(property.Name, property.PropertyType, property.GetValue(property));
                }
            }
        }

        private void PopulateParameters(IGH_Param param)
        {
            GH_AbstractParameter parameter = new GH_AbstractParameter(this, param)
            {
                Component = this,
                ParameterType = ParameterType.Relay,
                Sources = param.Sources.Select(source => source.InstanceGuid).ToList(),
                Targets = param.Recipients.Select(recipient => recipient.InstanceGuid).ToList(),
            };
            this.InputParameters.Add(parameter);
            this.OutputParameters.Add(parameter);
        }

        private void PopulateParameters(IGH_Component component)
        {
            foreach (IGH_Param param in component.Params.Input)
            {
                GH_AbstractParameter parameter = new GH_AbstractParameter(this, param)
                {
                    Component = this,
                    ParameterType = ParameterType.Input,
                    Sources = param.Sources.Select(source => source.InstanceGuid).ToList(),
                };
                this.InputParameters.Add(parameter);
            }

            foreach (IGH_Param param in component.Params.Output)
            {
                GH_AbstractParameter parameter = new GH_AbstractParameter(this, param)
                {
                    Component = this,
                    ParameterType = ParameterType.Output,
                    Targets = param.Recipients.Select(recipient => recipient.InstanceGuid).ToList(),
                };
                
                this.OutputParameters.Add(parameter);
            }
        }
        #endregion Methods
    }
}
