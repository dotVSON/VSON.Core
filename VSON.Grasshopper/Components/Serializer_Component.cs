using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace VSON.Grasshopper.Components
{
    public class Serializer_Component : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Serializer_Component class.
        /// </summary>
        public Serializer_Component()
          : base("Serializer_Component", "Nickname",
              "Description",
              "Util", "VSON")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Object to Serialize", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Json", "J", "Json Representation", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            object instanceObject = null;
            if (DA.GetData<object>(0, ref instanceObject))
            {
                if (this.Params.Input[0].SourceCount == 1)
                {
                    IGH_DocumentObject previousComponent = this.Params.Input[0].Sources[0].Attributes.GetTopLevel.DocObject;
                    this.SetIconOverride(previousComponent.Icon_24x24);
                    string json = Core.Serialization.Serializer.Serialize(previousComponent);
                    DA.SetData(0, json);
                }
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
            get { return new Guid("D7F44B5E-8BB8-4D24-B873-0C0DA74E8596"); }
        }
    }
}