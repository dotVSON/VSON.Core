using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace VSON.Grasshopper
{
    public class VSON_GrasshopperInfo : GH_AssemblyInfo
    {
        public override string Name => "VSON.Grasshopper";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("002bd4a6-217b-41b2-a848-eaf2cee35b39");

        //Return a string identifying you or your company.
        public override string AuthorName => "Kaushik LS";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}