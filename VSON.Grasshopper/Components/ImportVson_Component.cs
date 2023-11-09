using System;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;

namespace VSON.Grasshopper.Components
{
    public class ImportVson_Component : GH_Component
    {
        #region Constructor
        public ImportVson_Component() : base("ImportVson", "ImportVson", "", "Util", "VSON") { }
        #endregion Constructor

        #region Properties
        protected override System.Drawing.Bitmap Icon { get => null; }

        public override Guid ComponentGuid { get => new Guid("9745D548-A4BA-42E8-B41D-8F69801B54D3"); }
        #endregion Properties

        #region Methods
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new Param_FilePath(), "File", "F", "", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager) { }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = string.Empty;
            if (DA.GetData<string>(0, ref path))
            {
                //GH_AbstractDocument.Load(path, this.OnPingDocument());

                // Remove this component
                //this.OnPingDocument().RemoveObject(this, true);
            }
        }
        #endregion Methods
    }
}