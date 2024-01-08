using System;
using System.Drawing;
using System.Text;

namespace VSON.Core.Svg
{
    public class SvgLine : SvgBaseElement
    {
        #region Constructors
        public SvgLine() { }

        public SvgLine(double x1, double y1, double x2, double y2, SvgStyle style) : this()
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
            this.Style = style;
        }

        public SvgLine(PointF startPoint, PointF endPoint, SvgStyle style) : this(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y, style) { }
        #endregion Constructors

        #region Properties
        public double X1 { get; set; }

        public double X2 { get; set; }

        public double Y1 { get; set; }

        public double Y2 { get; set; }

        #endregion Properties

        #region Methods
        public override string ToXML()
        {
            return
                $" <line" +
                $" class=\"{this.Class}\"" +
                $" id=\"{this.Id}\"" +
                $" x1=\"{this.X1}\"" +
                $" y1=\"{this.Y1}\"" +
                $" x2=\"{this.X2}\"" +
                $" y2=\"{this.Y2}\"" +
                $" style=\"{this.Style.ToXML()}\"" +
                $" />";
        }
        #endregion Methods
    }
}
