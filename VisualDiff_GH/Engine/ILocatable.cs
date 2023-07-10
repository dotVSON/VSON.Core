using System;
using System.Drawing;

namespace VisualDiff_GH.Engine
{
    public interface ILocatable
    {
        string Type { get; set; }

        Guid ComponentGuid { get; set; }

        Guid InstanceGuid { get; set; }

        string Name { get; set; }

        string NickName { get; set; }

        bool Hidden { get; set; }

        bool Locked { get; set; }

        PointF Pivot { get; set; }

        //RectangleF Bounds { get; set; }

        SizeF Size { get; set; }
    }
}
