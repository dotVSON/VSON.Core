namespace VSON.Core.Svg
{
    public abstract class SvgBaseElement
    {
        public SvgStyle Style { get; set; } = null;

        public string Class { get; set; } = string.Empty;

        public string Id { get; set; } = string.Empty;

        

        public abstract string ToXML();
    }
}
