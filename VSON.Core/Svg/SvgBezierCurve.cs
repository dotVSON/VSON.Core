using System;
using System.Drawing;

namespace VSON.Core.Svg
{
    public class SvgBezierCurve : SvgBaseElement
    {
        #region Constructors
        public SvgBezierCurve() { }

        public SvgBezierCurve(PointF pointAtStart, PointF pointAtEnd, SvgStyle style)
        {
            this.PointAtStart = pointAtStart;
            this.PointAtEnd = pointAtEnd;
            this.Style = style;

            this.GenerateControlPoints();
        }
        #endregion Constructors

        #region Properties
        public PointF PointAtStart { get; set; } = PointF.Empty;
        
        public PointF PointAtEnd { get; set; } = PointF.Empty;

        public PointF C1 { get; private set; } = PointF.Empty;

        public PointF C2 { get; private set; } = PointF.Empty;
        
        public SvgStyle Style { get; set; } = null;

        #endregion Properties

        #region Methods
        public void GenerateControlPoints(float projectionFactor = 150)
        {
            if (this.PointAtStart.IsEmpty == false && this.PointAtEnd.IsEmpty == false)
            {
                double avgX = 0.5 * (this.PointAtStart.X + this.PointAtEnd.X);

                this.C1 = new PointF()
                {
                    X = (float)(avgX + projectionFactor),
                    Y = this.PointAtStart.Y,
                };

                this.C2 = new PointF()
                {
                    X = (float)(avgX - projectionFactor),
                    Y = this.PointAtEnd.Y,
                };
            }
            else
            {
                throw new InvalidOperationException("Cannot calculate Control Points. Start and End Points are empty.");
            }
        }
        public override string ToXML()
        {
            return
                $" <path d=\"" +
                $" M {this.PointAtStart.X} {this.PointAtStart.Y}" +
                $" C {this.C1.X} {this.C1.Y}, {this.C2.X} {this.C2.Y}, {this.PointAtEnd.X} {this.PointAtEnd.Y}\"" +
                $" style=\"{this.Style.ToXML()}\"" +
                $" />";
        }
        #endregion Methods
    }
}
