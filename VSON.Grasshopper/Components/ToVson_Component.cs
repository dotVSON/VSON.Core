using System;
using System.Collections.Generic;
using VSON;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace VSON.Grasshopper.Components
{
    public class ToVson_Component : GH_Component
    {
        #region Constructor
        public ToVson_Component() : base("VSON Serialize", "VSON", "Serialize component to VSON format.", "Util", "VSON") { }
        #endregion Constructor

        #region Properties
        protected override System.Drawing.Bitmap Icon { get => null; }

        public override Guid ComponentGuid { get => new Guid("70ECCB60-1897-48AF-B8E3-45F9C6BE17FE"); }
        #endregion Properties

        #region Methods
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Component", "C", "Component to serialize", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Vson", "V", "Serialised in Vson format.", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            if (this.Params.Input[0].SourceCount > 0)
            {
                IGH_DocumentObject documentObject = this.Params.Input[0].Sources[0].Attributes.GetTopLevel.DocObject;
                
                if (documentObject is IGH_Component component)
                {
                    GH_AbstractComponent abstractComponent = new GH_AbstractComponent(component);
                    DA.SetData(0, abstractComponent.Serialize());
                }
                else if (documentObject is IGH_Param param)
                {
                    GH_AbstractComponent abstractComponent = new GH_AbstractComponent(param);
                    DA.SetData(0, abstractComponent.Serialize());
                }
            }
        }
        #endregion Methods
    }
}