using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSON.Core.Render
{
    public class SVGSettings
    {
        public static SVGSettings Default { get =>  new SVGSettings(); }

        private SVGSettings() { }
        public SVGSettings(string fill, string stroke, string strokeWidth) : this()
        {
            this.Fill = fill;
            this.Stroke = stroke;
            this.StrokeWidth = strokeWidth;
        }

        public string Fill { get; set; } = "#f0f0f0";
        public string Stroke { get; set; } = "black";
        public string StrokeWidth { get; set; } = "2";

        public string GetStyle()
        {
            string style = $"style = \"fill: {this.Fill}; stroke: {this.Stroke}; stroke-width: {this.StrokeWidth};\"";
            return style;
        }   
    }
}
