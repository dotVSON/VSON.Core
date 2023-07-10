using Grasshopper.Kernel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VisualDiff_GH.Engine;

namespace VisualDiff_GH.Components
{
    public class FileDiff_Component : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the DiffFile_Component class.
        /// </summary>
        public FileDiff_Component()
          : base("FileDiff", "FileDiff",
              "Description",
              "Dev", "Util")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("File", "F", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Component> components = new List<Component>();
            foreach (var c in this.OnPingDocument().ActiveObjects())
            {
                if (c is IGH_Component component)
                {
                    if (component.InstanceGuid != this.InstanceGuid)
                    {
                        Component diffComponent = new Component(component);
                        components.Add(diffComponent);
                    }
                }

            }

            string serialised = JsonConvert.SerializeObject(components, Formatting.Indented);

            DA.SetData(0, serialised);
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
            get { return new Guid("DA9A0A46-E446-46A3-BA0F-D7B88593BE86"); }
        }
    }
}