using Grasshopper;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Newtonsoft.Json;
using System;
using System.Linq;
using VSON.Core;
using VSON.Grasshopper.Components;

namespace VSON.Grasshopper
{
    public class GH_AbstractDocument : Document
    {
        #region Constructor

        [JsonConstructor]
        private GH_AbstractDocument() : base()
        {
            this.Discriminator = this.GetType().FullName;
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
        public override void LoadFromDocument(string path, object documentObject = null)
        {
            if (documentObject is GH_Document document)
            {
                if (document == null)
                {
                    document = new GH_Document();
                    Instances.DocumentServer.AddDocument(document);
                }

                GH_AbstractDocument vsonDocument = JsonConvert.DeserializeObject<GH_AbstractDocument>(path);

                foreach (Component component in vsonDocument.ComponentTable.Values)
                {
                    IGH_DocumentObject emittedObject = Instances.ComponentServer.EmitObject(component.ComponentGuid);
                    if (emittedObject != null)
                    {
                        document.AddObject(emittedObject, false);
                        emittedObject.Attributes.Pivot = component.Pivot;
                        // Restore all other properties, attributes, etc.
                        // Restore Wires in the end.
                    }
                }

                /*foreach (Wire wire in vsonDocument.WireTable.Values)
                {
                    // Connect Wires
                }*/

                document.ScheduleSolution(1);
            }
            else
            {
                throw new ArgumentException("Object needs to be of type Grasshopper.Kernel.GH_Document.");
                
            }
        }

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
