namespace VSON.Core.Svg
{
    public abstract class SvgBaseElement
    {
        public SvgStyle Style { get; set; }

        public abstract string ToXML();
    }
}
