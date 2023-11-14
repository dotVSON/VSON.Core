using Newtonsoft.Json;
using System;
using System.Drawing;
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
            this.SourceGuid = sourceGuid;
            this.TargetGuid = targetGuid;

            this.Initialize(document);
        }
        #endregion Constructor

        #region Properties

        public Guid SourceGuid { get; set; }

        public Guid TargetGuid { get; set; }

        public DiffState DiffState { get; set; } = DiffState.Unknown;

        public Parameter SourceParameter { get => this.ActiveDocument.ParameterTable[this.SourceGuid]; }

        public Parameter TargetParameter { get => this.ActiveDocument.ParameterTable[this.TargetGuid]; }

        #endregion Properties

        #region Methods
        public void Initialize(Document document)
        {
            this.InstanceGuid = Guid.NewGuid();
            document.Register(this);
        }

        public override string DrawSVG()
        {
            SvgStyle wireStyle = new SvgStyle()
            {
                Fill = "none",
                Stroke = "black",
                StrokeWidth = 2,
            };

            PointF pointAtStart = new PointF()
            {
                X = (float)(this.SourceParameter.Bounds.Right + 2),
                Y = (float)(this.SourceParameter.Bounds.Bottom - this.SourceParameter.Bounds.Height * 0.5),
            };
            PointF pointAtEnd = new PointF()
            {
                X = (float)(this.TargetParameter.Bounds.X - 2),
                Y = (float)(this.TargetParameter.Bounds.Bottom - this.TargetParameter.Bounds.Height * 0.5)
            };

            SvgBezierCurve wireCurve = new SvgBezierCurve(pointAtStart, pointAtEnd, wireStyle);
            return wireCurve.ToXML();

            /*SvgLine wireLine = new SvgLine()
            {
                X1 = this.SourceParameter.Bounds.Right + 2,
                Y1 = this.SourceParameter.Bounds.Bottom - this.SourceParameter.Bounds.Height * 0.5,
                
                X2 = this.TargetParameter.Bounds.X - 2,
                Y2 = this.TargetParameter.Bounds.Bottom - this.TargetParameter.Bounds.Height * 0.5,

                Style = wireStyle,
            };*/
        }
        #endregion Methods
    }
}