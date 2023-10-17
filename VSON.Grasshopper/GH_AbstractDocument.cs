using Grasshopper.Kernel;
using Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.GUI.Canvas;
using System.Drawing;
using VSON.Core;

namespace VSON.Grasshopper
{
    public class GH_AbstractDocument : VsonDocument
    {
        #region Constructor
        private GH_AbstractDocument() : base()
        {
            GH_Canvas activeCanvas = Instances.ActiveCanvas;
            this.CanvasLocation = new SizeF(activeCanvas.Viewport.VisibleRegion.X, activeCanvas.Viewport.VisibleRegion.Y);
            this.CanvasZoom = activeCanvas.Viewport.Zoom;

            this.Components = new List<VsonComponent>();
        }

        public GH_AbstractDocument(GH_Document document) : this()
        {
            this.Name = document.DisplayName;
            this.Id = document.DocumentID;
            this.FilePath = document.FilePath;
            
            foreach (IGH_DocumentObject documentObject in document.ActiveObjects())
            {
                this.Components.Add(GH_AbstractComponent.CreateFromDocumentObject(documentObject));
            }
        }
        #endregion Constructor

        #region Methods
        public bool Save() => this.SerializeToFile();

        public bool Save(string path) => this.SerializeToFile(path);

        public static void Load(string path, GH_Document document = null)
        {
            if (document == null)
            {
                document = new GH_Document();
                Instances.DocumentServer.AddDocument(document);
            }

            VsonDocument vsonDoc = VsonDocument.DeserializeFromFile<VsonDocument>(path);
            foreach (VsonComponent component in vsonDoc.Components)
            {
                if (component.ComponentType == VsonComponentType.GrasshopperComponent)
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
        #endregion Methods
    }
}
