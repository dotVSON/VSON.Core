using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace VSON.Grasshopper.Components
{
    public class FromVson_Component : GH_Component
    {
        #region Constructor
        public FromVson_Component() : base("VSON DeSerialize", "VSON", "DeSerialize VSON to Component.", "Util", "VSON") { }
        #endregion Constructor

        #region Properties
        protected override System.Drawing.Bitmap Icon { get => null; }

        public override Guid ComponentGuid { get => new Guid("61A2BE3A-5A3B-45F5-9EB7-43995256C6AA"); }
        #endregion Properties

        #region Methods
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("VSON", "V", "", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Component", "C", "", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string jsonText = string.Empty;
            if (DA.GetData<string>(0, ref jsonText))
            {
                object comp = GH_AbstractComponent.Deserialze<GH_AbstractComponent>(jsonText);
                DA.SetData(0, comp);
            }
        }
        #endregion Methods
    }
}