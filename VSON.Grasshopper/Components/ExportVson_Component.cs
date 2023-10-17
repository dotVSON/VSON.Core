using System;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;

namespace VSON.Grasshopper.Components
{
    public class ExportVson_Component : GH_Component
    {
        #region Constructor
        public ExportVson_Component() : base("ExportVson", "ExportVson", "", "Util", "VSON") { }
        #endregion Constructor

        #region Properties
        protected override System.Drawing.Bitmap Icon { get => null; }

        public override Guid ComponentGuid { get => new Guid("DEFB4441-3E03-4C72-9C56-3BEA71025665"); }
        #endregion Properties

        #region Methods
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Save", "S", "", GH_ParamAccess.item);
            pManager.AddParameter(new Param_FilePath(), "Path", "P", "", GH_ParamAccess.item);
            pManager[1].Optional = true;
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager) { }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool trigger = false;
            if (DA.GetData<bool>(0, ref trigger))
            {
                GH_AbstractDocument vsonDoc = new GH_AbstractDocument(this.OnPingDocument());
                
                string path = string.Empty;
                if (DA.GetData<string>(1, ref path))
                {
                    vsonDoc.Save(path);
                }
                else
                {
                    vsonDoc.Save();
                }
            }
        }
        #endregion Methods
    }
}
