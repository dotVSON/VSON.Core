using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSON.Core.Render
{
    public class SVG
    {
        private string svgHeader;

        public SVG() { }

        public SVG(int width, int height, RectangleF viewRectangle, string fillColor="white")
        {
            this.svgHeader = $"<svg width=\"{width}\" height=\"{height}\" viewBox=\"{viewRectangle.Left}\" \"{viewRectangle.Bottom}\" \"{viewRectangle.Width}\" \"{viewRectangle.Height}\" fill=\"{fillColor}\"";
        }


        #region Properties
        public int Width { get; set; } = 1920;
        public int Height { get; set; } = 1080;
        public RectangleF ViewRectangle { get; set;}
        public string FillColor { get; set; } = "white";

        public static string componentBaseStyle { get => "style = \"fill: #f0f0f0; stroke: black; stroke-width:2;\""; }
        #endregion Properties

        #region Methods
        public string FromComponent(VsonComponent component)
        {
            int
                filletRadius = 5,
                circleRadius = 8;
            string
                fill = "#fofofo",
                stroke = "black",
                strokeWdith = "2";

            string componentRectangle = $"<rect" +
                $"x=\"{component.Pivot.X}" +
                $"y=\"{component.Pivot.Y}" +
                $"rx=\"{filletRadius}" +
                $"ry=\"{filletRadius}" +
                $"width=\"{component.Bounds.Width}" +
                $"height=\"{component.Bounds.Height}" +
                $"style=\"{SVGSettings.Default.GetStyle()}";

            
            

            return componentRectangle;
        }

        public static string DrawCircle(double x = 0, double y = 0, double r = 5)
        {
            string sml = $"<circle cs=\"\"";
            
            
            
            return "";
        }
        #endregion Methods

    }
}
