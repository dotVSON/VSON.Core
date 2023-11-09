using Newtonsoft.Json;
using System;
using VSON.Core.Svg;

namespace VSON.Core
{
    [Serializable]
    public class Wire : CanvasElement, IDiff
    {
        #region Constructor
        [JsonConstructor]
        public Wire() { }

        public Wire(Document document, Guid sourceGuid, Guid targetGuid) : this()
        {
            document.Register(this);

            this.SourceGuid = sourceGuid;
            this.TargetGuid = targetGuid;
        }

        /*public Wire(Parameter sourceParameter, Parameter targetParameter) : this()
        {
            this.SourceParameter = sourceParameter;
            this.TargetParameter = targetParameter;
        }*/
        #endregion Constructor

        #region Properties

        public Guid SourceGuid { get; set; }

        public Guid TargetGuid { get; set; }

        public DiffState DiffState { get; set; } = DiffState.Unknown;

        public Parameter SourceParameter { get => this.ActiveDocument.ParameterTable[this.SourceGuid]; }

        public Parameter TargetParameter { get => this.ActiveDocument.ParameterTable[this.TargetGuid]; }

        //[JsonIgnore] public Component SourceComponent { get => this.SourceParameter?.Component; }

        //[JsonIgnore] public Component TargetComponent { get => this.TargetParameter?.Component; }
        #endregion Properties

        #region Methods
        public override string DrawSVG()
        {
            SvgStyle wireStyle = new SvgStyle()
            {
                Fill = "none",
                Stroke = "black",
                StrokeWidth = 2,
            };

            SvgBezierCurve wireCurve = new SvgBezierCurve()
            {
                PointAtStart = this.SourceParameter.Pivot,
                PointAtEnd = this.TargetParameter.Pivot,
                Style = wireStyle,
            };
            return wireCurve.ToXML();
        }
        #endregion Methods
    }
}