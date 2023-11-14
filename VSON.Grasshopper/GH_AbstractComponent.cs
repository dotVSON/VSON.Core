using System.Linq;
using Grasshopper.Kernel;
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

        #region Methods  
        private void Initialize(GH_AbstractDocument document, IGH_DocumentObject documentObject)
        {
            this.Type = documentObject.GetType().FullName;
            this.ComponentGuid = documentObject.ComponentGuid;
            this.InstanceGuid = documentObject.InstanceGuid;
            this.Name = documentObject.Name;
            this.NickName = documentObject.NickName;
            this.Pivot = documentObject.Attributes.Pivot;
            this.Bounds = documentObject.Attributes.Bounds;

            document.Register(this);
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
