using Grasshopper.Kernel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace VisualDiff_GH.Engine
{
    [Serializable]
    public class Component : ILocatable
    {
        #region Constructors
        [JsonConstructor]
        private Component() { }
        public Component(IGH_Component component) : this()
        {
            this.Type = component.GetType().FullName;
            this.ComponentGuid = component.ComponentGuid;
            this.InstanceGuid = component.InstanceGuid;
            this.Name = component.Name;
            this.NickName = component.NickName;
            this.Message = component.Message;
            this.Hidden = component.Hidden;
            this.Locked = component.Locked;
            this.Pivot = component.Attributes.Pivot;
            this.Bounds = component.Attributes.Bounds;

            this.Size = new SizeF(component.Attributes.Bounds.Width, component.Attributes.Bounds.Height);

            this.InputParams = new List<Param>();
            this.OutputParams = new List<Param>();

            foreach (IGH_Param param in component.Params.Input)
            {
                Param newParam = new Param(param);
                this.InputParams.Add(newParam);
            }

            foreach (IGH_Param param in component.Params.Output)
            {
                Param newParam = new Param(param);
                this.OutputParams.Add(newParam);
            }
        }
        #endregion Constructors

        #region Properties
        public string Type { get; set; } = "MissingComponent";
        public Guid ComponentGuid { get; set; } = Guid.Empty;
        public Guid InstanceGuid { get; set; } = Guid.Empty;
        public string Name { get; set; } = "UntitledComponent";
        public string NickName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool Hidden { get; set; } = false;
        public bool Locked { get; set; } = false;
        public PointF Pivot { get; set; } = PointF.Empty;
        public RectangleF Bounds { get; set; } = RectangleF.Empty;
        public SizeF Size { get; set; } = SizeF.Empty;

        public List<Param> InputParams { get; set; } = new List<Param>();
        public List<Param> OutputParams { get; set; } = new List<Param>();
        #endregion Properties
    }
}
