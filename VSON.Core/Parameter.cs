using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace VSON.Core
{
    public enum ParameterType
    {
        Output = 0,
        Input = 1,
        Relay = 2,
    }

    public class Parameter : CanvasElement, IDiff
    {
        #region Properties
        [JsonIgnore] public Component Component { get; set; }

        public DiffState DiffState { get; set; } = DiffState.Unknown;

        public Guid ComponentGuid { get; set; }

        public ParameterType ParameterType { get; set; }

        public List<Guid> Sources { get; set; }

        public List<Guid> Targets { get; set; }

        [JsonIgnore] public List<Wire> IncomingWires { get; set; }

        [JsonIgnore] public List<Wire> OutgoingWires { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }

        public string Message { get; set; } = string.Empty;

        public bool IsHidden { get; set; }

        public bool IsLocked { get; set; }

        public PointF Pivot { get; set; }

        public SizeF Size { get => new SizeF(this.Bounds.Width, this.Bounds.Height); }
        #endregion Properties

        #region Methods
        #endregion Methods
    }
}
