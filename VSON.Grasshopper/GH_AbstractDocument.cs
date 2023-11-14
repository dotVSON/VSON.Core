using Grasshopper;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Newtonsoft.Json;
using System;
using System.Linq;
using VSON.Core;

namespace VSON.Grasshopper
{
    public class GH_AbstractDocument : Document
    {
        #region Constructor

        [JsonConstructor]
        private GH_AbstractDocument() : base()
        {
            this.Type = this.GetType().FullName;
            GH_Canvas activeCanvas = Instances.ActiveCanvas;
            this.CanvasZoom = activeCanvas.Viewport.Zoom;
            this.CanvasLocation = new System.Drawing.SizeF(activeCanvas.Viewport.VisibleRegion.X, activeCanvas.Viewport.VisibleRegion.Y);
        }

        /// <summary>
        /// Creates an Abstract representation of the Grasshopper Document.
        /// </summary>
        /// <param name="document"></param>
        public GH_AbstractDocument(GH_Document document) : this()
        {
            this.Name = document.DisplayName;
            this.InstanceGuid = document.DocumentID;
            this.FilePath = document.FilePath;

            this.PopulateComponentTable(document);
            this.PopulateWireTable();
        }

        #endregion Constructor

        #region Methods
        public void PopulateGraph(GH_Document document)
        {
            this.PopulateComponentTable(document);
            this.PopulateWireTable();
        }

        public void PopulateComponentTable(GH_Document document)
        {
            foreach (IGH_DocumentObject docObject in document.ActiveObjects())
            {
                if (docObject is IGH_Component docComponent)
                {
                    GH_AbstractComponent component = new GH_AbstractComponent(this, docComponent);
                }

                else if (docObject is IGH_Param docParam)
                {
                    if (GH_AbstractParameter.IsStandaloneComponent(docParam))
                    {
                        GH_AbstractComponent component = new GH_AbstractComponent(this, docParam);
                    }
                    else
                    {
                        // Code should never go here.
                        throw new NotImplementedException("ScriptError");
                    }
                }

                else // Neither here
                {
                    throw new NotImplementedException("ScriptError");
                    // Are there any other objects?
                    // If yes, What happens to them?
                }
            }
        }

        public void PopulateWireTable()
        {

            foreach (GH_AbstractComponent component in this.ComponentTable.Values.Cast<GH_AbstractComponent>())
            {
                foreach (GH_AbstractParameter parameter in component.OutputParameters.Cast<GH_AbstractParameter>())
                {
                    foreach (Guid id in parameter.Targets)
                    {
                        Wire incomingWire = new Wire(this, parameter.InstanceGuid, id);
                    }
                }

                foreach (GH_AbstractParameter parameter in component.InputParameters.Cast<GH_AbstractParameter>())
                {
                    foreach (Guid id in parameter.Sources)
                    {
                        Wire incomingWire = new Wire(this, id, parameter.InstanceGuid);
                    }
                }
            }
        }
        #endregion Methods

        #region WipMethods

        /*
        public bool Save() => this.SerializeToFile();

        public bool Save(string path) => this.SerializeToFile(path);
        
        public static void Load(string path, GH_Document document = null)
        {
            if (document == null)
            {
                document = new GH_Document();
                Instances.DocumentServer.AddDocument(document);
            }
            VsonDocument_Old vsonDoc = VsonDocument_Old.DeserializeFromFile<VsonDocument_Old>(path);
            foreach (Component component in vsonDoc.Components)
            {
                if (component.ComponentType == ComponentType.GrasshopperComponent)
                {
                    IGH_DocumentObject emittedObject = Instances.ComponentServer.EmitObject(component.ComponentGuid);
                    if (emittedObject != null)
                    {
                        document.AddObject(emittedObject, false);
                        emittedObject.Attributes.Pivot = component.Pivot;
                    }
                    else
                    {

                    }
                }
            }
            document.ScheduleSolution(1);
        }
        */
        #endregion WipMethods
    }
}
