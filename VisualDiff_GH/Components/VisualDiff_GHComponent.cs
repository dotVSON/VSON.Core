using Grasshopper.Kernel;
using Newtonsoft.Json;
using System;
using VisualDiff_GH.Engine;

namespace VisualDiff_GH.Components
{
    public class VisualDiff_GHComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public VisualDiff_GHComponent()
          : base("VisualDiff", "Diff",
            "Description",
            "Dev", "Util")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Component", "C", "Component to mirror.", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Mirror", "M", "Mirrored Component.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            if (this.Params.Input[0].SourceCount > 0)
            {
                var comp = this.Params.Input[0].Sources[0].Attributes.GetTopLevel.DocObject;

                if (comp is IGH_Component compo)
                {
                    Component mirrorComponent = new Component(compo);

                    string serialize = JsonConvert.SerializeObject(mirrorComponent, Formatting.Indented);



                    DA.SetData(0, serialize);
                }


            }
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon => null;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("0602f5f7-0013-4fbe-855a-cd6df95a87a3");
    }
}