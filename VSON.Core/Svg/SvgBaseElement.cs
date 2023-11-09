namespace VSON.Core.Svg
{
    public abstract class SvgBaseElement
    {
        public SvgStyle Style { get; set; } = null;

        public abstract string ToXML();
    }
}
