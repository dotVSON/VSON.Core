using System.Drawing;

namespace VSON.Core.Svg
{
    public class SvgCircle : SvgBaseElement
    {
        #region Constructors
        public SvgCircle() { }

        public SvgCircle(double x, double y, double radius) : this()
        {
            this.X = x;
            this.Y = y;
            this.Radius = radius;
        }

        public SvgCircle(double x, double y, double radius, SvgStyle style) : this(x, y, radius)
        {
            this.Style = style;
        }

        public SvgCircle(PointF origin, double radius, SvgStyle style) : this(origin.X, origin.Y, radius, style) { }
        #endregion Constructors

        #region Properties
        public double X { get; set; } = 50;

        public double Y { get; set; } = 50;
        
        public double Radius { get; set; } = 10;

        public SvgStyle Style { get; set; } = null;
        #endregion Properties

        #region Methods
        public override string ToXML()
        {
            return
                $" <circle" +
                $" cx=\"{this.X}\"" +
                $" cy=\"{this.Y}\"" +
                $" r=\"{this.Radius}\"" +
                $" style=\"{this.Style.ToXML()}\"" +
                $" />";
        }
        #endregion Methods
    }
}
