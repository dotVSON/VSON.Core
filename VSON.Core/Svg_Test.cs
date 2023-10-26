using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSON.Core
{
    public class Svg_Test
    {
        public static string DrawCircle(double x, double y, double radius, string stroke, double strokeWidth, string fill)
        {
            string code = $"<circle cx=\"{x}\" cy=\"{y}\" r=\"{radius}\"" +
               $"stroke=\"{stroke}\" stroke-width=\"{strokeWidth}\" fill=\"{fill}\" />";
            return code;
        }


        public static void DrawRectangle()
        {


        }

    }
}
