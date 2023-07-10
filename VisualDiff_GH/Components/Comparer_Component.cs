using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using VisualDiff_GH.Engine;

namespace VisualDiff_GH.Components
{
    public class Comparer_Component : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Comparer_Component class.
        /// </summary>
        public Comparer_Component()
          : base("Comparer", "Comparer",
              "Description",
              "Dev", "Util")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Left", "L", "", GH_ParamAccess.item);
            pManager.AddTextParameter("Right", "R", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Added", "A", "", GH_ParamAccess.list);
            pManager.AddGenericParameter("Removed", "R", "", GH_ParamAccess.list);
            pManager.AddGenericParameter("Retained", "T", "", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string
                leftFile = string.Empty,
                rightFile = string.Empty;
            if (DA.GetData<string>(0, ref leftFile) && DA.GetData<string>(1, ref rightFile))
            {
                /*string path = @"A:\GitHub\VisualDiff\.local\sample.json";

                string text = System.IO.File.ReadAllText(path);

                try
                {
                    var contents = JsonConvert.DeserializeObject<DiffFile>(text);
                }

                catch(Exception ex)
                {
                    Rhino.RhinoApp.WriteLine(ex.Message);
                }*/

                List<Component>[] outputs = Comparer.CompareJson(leftFile, rightFile);

                DA.SetDataList(0, outputs[0]);
                DA.SetDataList(1, outputs[1]);
                DA.SetDataList(2, outputs[2]);

            }

        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("0245964F-0A7B-4DEC-BBD3-D96429E99C6B"); }
        }
    }
}