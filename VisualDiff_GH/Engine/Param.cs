using Grasshopper.Kernel;
using Newtonsoft.Json;
using System;
using System.Drawing;


namespace VisualDiff_GH.Engine
{
    [Serializable]
    public class Param : ILocatable
    {
        #region Constructors
        [JsonConstructor]
        private Param() { }
        public Param(IGH_Param param) : this()
        {
            this.Type = param.GetType().FullName;
            this.ComponentGuid = param.ComponentGuid;
            this.InstanceGuid = param.InstanceGuid;
            this.Name = param.Name;

            this.Locked = param.Locked;
            this.Pivot = param.Attributes.Pivot;
            //this.Bounds = param.Attributes.Bounds;
            this.Size = new SizeF(param.Attributes.Bounds.Width, param.Attributes.Bounds.Height);
            this.Kind = (int)param.Kind;

            this.Access = (int)param.Access;
            this.Optional = param.Optional;

            this.Reverse = param.Reverse;
            this.Simplify = param.Simplify;
            this.WireDisplay = (int)param.WireDisplay;

            this.Owner = param.Attributes.GetTopLevel.DocObject.InstanceGuid;

            // PersistentData
            // VolatileData
            // Sources
            // Recipents
        }
        #endregion Constructors

        #region Properties
        public string Type { get; set; } = "MissingComponent";

        public Guid ComponentGuid { get; set; } = Guid.Empty;

        public Guid InstanceGuid { get; set; } = Guid.Empty;

        public string Name { get; set; } = "UntitledParameter";

        public string NickName { get; set; } = String.Empty;

        public bool Hidden { get; set; } = false;

        public bool Locked { get; set; } = false;

        public PointF Pivot { get; set; } = new PointF(0, 0);

        public RectangleF Bounds { get; set; } = RectangleF.Empty;

        public SizeF Size { get; set; } = SizeF.Empty;

        /// <summary>
        /// unknown = 0
        /// floating = 1
        /// input = 2
        /// output = 3
        /// </summary>
        public int Kind { get; set; } = 0;

        /// <summary>
        /// item = 0
        /// list = 1
        /// tree = 2
        /// </summary>
        public int Access { get; set; } = 0;

        public bool Optional { get; set; } = false;

        public bool Reverse { set; get; } = false;

        public bool Simplify { set; get; } = false;

        /// <summary>
        /// default = 0
        /// faint = 1
        /// hidden = 2
        /// </summary>
        public int WireDisplay { get; set; } = 0;

        public Guid Owner { get; set; } = Guid.Empty;
        #endregion Properties
    }
}
