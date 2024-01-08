namespace VSON.Core.Svg
{
    public class SvgText : SvgBaseElement
    {
        #region Constructor
        public SvgText() { }

        public SvgText(double x, double y, SvgStyle style, string fontFamily = "monospaced", double fontSize = 12) : this()
        {
            this.X = x;
            this.Y = y;
            this.FontFamily = fontFamily;
            this.FontSize = fontSize;
            this.Style = style;
        }
        #endregion Constructor

        #region Properties
        public double X { get; set; }
        public double Y { get; set; }
        public string FontFamily { get; set; } = "monospaced";
        public double FontSize { get; set; }

        #endregion Properties

        #region Methods
        public override string ToXML()
        {
            return
                $" <text" +
                $" class=\"{this.Class}\"" +
                $" id=\"{this.Id}\"" +
                $" x=\"{this.X}\"" +
                $" x=\" {this.X}\"" +
                $" font-family=\" {this.FontFamily}\"" +
                $" font-size=\"{this.FontSize}\"" +
                $" style=\"{this.Style.ToXML()}\"" +
                $" />";
        }
        #endregion Methods
    }
}
