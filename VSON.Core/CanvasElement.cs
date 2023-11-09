using Newtonsoft.Json;
using System;
using System.Drawing;

namespace VSON.Core
{
    [Serializable]
    public class CanvasElement
    {
        public string Type { get; set; }

        [JsonIgnore] public Document ActiveDocument { get; set; }

        public Guid InstanceGuid { get; set; }

        public virtual RectangleF Bounds { get; set; }

        public virtual string DrawSVG()
        {
            throw new NotImplementedException();

            /*SvgStyle style = new SvgStyle()
            {
                Fill = "white",
                Stroke = "black",
                StrokeWidth = 2,
            };

            SvgRectangle componentRectangle = new SvgRectangle(this.Bounds, style)
            {
                XRadius = 5,
                YRadius = 5,
            };

            return componentRectangle.ToXML();*/
        }

        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
