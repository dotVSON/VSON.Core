using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace VisualDiff_GH
{
    public class VisualDiff_GHInfo : GH_AssemblyInfo
    {
        public override string Name => "VisualDiff_GH";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("ad7bd06f-ea87-48a0-b62e-9b69c910b310");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}