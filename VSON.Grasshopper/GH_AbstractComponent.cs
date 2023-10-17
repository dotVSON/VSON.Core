using System.Collections.Generic;
using Grasshopper.Kernel;
using VSON.Core;

namespace VSON.Grasshopper
{
    public class GH_AbstractComponent : VsonComponent
    {
        #region Constructor
        private GH_AbstractComponent() : base() { }

        private GH_AbstractComponent(IGH_DocumentObject documentObject) : this()
        {
            this.ComponentType = VsonComponentType.GenericComponent;
            this.Initialize(documentObject);
        }

        public GH_AbstractComponent(IGH_Component component) : this(component as IGH_DocumentObject)
        {
            this.ComponentType = VsonComponentType.GrasshopperComponent;
            this.Locked = component.Locked;
            this.Hidden = component.Hidden;
            this.Message = component.Message;

            foreach (IGH_Param param in component.Params.Input)
            {
                GH_AbstractComponent newParam = new GH_AbstractComponent(param);
                this.InputParams.Add(newParam);
            }

            foreach (IGH_Param param in component.Params.Output)
            {
                GH_AbstractComponent newParam = new GH_AbstractComponent(param);
                this.OutputParams.Add(newParam);
            }
        }

        public GH_AbstractComponent(IGH_Param param) : this(param as IGH_DocumentObject)
        {
            this.ComponentType = VsonComponentType.GrasshopperParam;
            this.Locked = param.Locked;
            this.Hidden = false;
        }

        #endregion Constructor

        #region Methods
        public static GH_AbstractComponent CreateFromDocumentObject(IGH_DocumentObject documentObject)
        {
            if (documentObject is IGH_Component component)
            {
                return new GH_AbstractComponent(component);
            }
            else if (documentObject is IGH_Param param)
            {
                return new GH_AbstractComponent(param);
            }
            else
            {
                return new GH_AbstractComponent(documentObject);
            }
        }
        private void Initialize(IGH_DocumentObject documentObject)
        {
            this.InputParams = new List<VsonComponent>();
            this.OutputParams = new List<VsonComponent>();

            this.Type = documentObject.GetType().FullName;
            this.ComponentGuid = documentObject.ComponentGuid;
            this.InstanceGuid = documentObject.InstanceGuid;
            this.Name = documentObject.Name;
            this.NickName = documentObject.NickName;
            this.Pivot = documentObject.Attributes.Pivot;
            this.Bounds = documentObject.Attributes.Bounds;
        }
        #endregion Methods
    }
}
