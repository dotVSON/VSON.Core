using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using Grasshopper.GUI.Base;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Rhino.Geometry;
using System.CodeDom;
using System.Linq;
using Grasshopper.Kernel.Types;

namespace VSON.Grasshopper.Components
{
    public class Deserializer_Component : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Deserializer_Component class.
        /// </summary>
        public Deserializer_Component()
          : base("Deserializer_Component", "Nickname",
              "Description",
              "Util", "VSON")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("JSON", "J", "", GH_ParamAccess.item);
            pManager.AddTextParameter("Name", "N", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string
                json = string.Empty,
                name = string.Empty;
            if (DA.GetData<string>(0, ref json) && DA.GetData<string>(1, ref name))
            {
                Type type = Type.GetType(name);
                PointF newPivot = new PointF(500, 500);

                var o = JObject.Parse(json)["item"];
                var sliderBase = JsonConvert.DeserializeObject(o.ToString(), type);
                
                DA.SetData(0, sliderBase);
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
            get { return new Guid("FFB58D26-B2BC-47B7-ACD1-4275E4A3AF19"); }
        }
    }
}