using Grasshopper.Kernel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VSON.Core;

namespace VSON.Grasshopper
{
    public class GH_AbstractParameter : Parameter
    {
        #region Constructor
        [JsonConstructor]
        private GH_AbstractParameter() { }

        public GH_AbstractParameter(GH_AbstractComponent component) : this()
        {
            this.Component = component;
            this.Component.ActiveDocument.Register(this);
        }

        public GH_AbstractParameter(GH_AbstractComponent component, IGH_Param parameter) : this(component)
        {
            this.Message = string.Empty;
            this.IsHidden = false;
            this.IsLocked = false;

            this.Initialize(parameter);
        }

        #endregion Constructor

        #region Methods
        private void Initialize(IGH_DocumentObject documentObject)
        {
            this.Type = this.GetType().FullName;
            this.ComponentGuid = documentObject.ComponentGuid;
            this.InstanceGuid = documentObject.InstanceGuid;
            this.Name = documentObject.Name;
            this.NickName = documentObject.NickName;
            this.Pivot = documentObject.Attributes.Pivot;
            this.Bounds = documentObject.Attributes.Bounds;
        }

        internal static bool IsStandaloneComponent(IGH_Param param)
        {
            IGH_DocumentObject component = param.Attributes.GetTopLevel.DocObject;
            return component.GetType().FullName == param.GetType().FullName;
        }
        #endregion Methods
    }
}
