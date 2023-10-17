using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSON.Diff
{
    [Serializable]
    public class DiffChange
    {
        public DiffChange() { }

        public string Attribute { get; set; }
        public VsonDiffState State { get; set; }

        public DiffChange(string attribute,VsonDiffState state)
        { 
            this.Attribute = attribute;
            this.State = state;
        }
    }

    [Serializable]
    public class DocumentComparer
    {
        #region Constructor
        private DocumentComparer() { }

        public DocumentComparer(VsonDocument documentA, VsonDocument documentB) : this()
        {
            this.LeftDocument = documentA;
            this.RightDocument = documentB;
        }
        #endregion Constructor

        #region Properties
        public VsonDocument LeftDocument { get; set; }
        public VsonDocument RightDocument { get; set; }
        #endregion Properties

        #region Methods
        public List<DiffChange> Compare()
        {
            List<DiffChange> changes = new List<DiffChange>();
            changes.AddRange(this.CompareAttributes());
            changes.AddRange(this.CompareComponents());
            return changes;
        }

        public IEnumerable<DiffChange> CompareAttributes()
        {
            if (this.LeftDocument.Id != this.RightDocument.Id)
            {
                yield return new DiffChange("Document.Id", VsonDiffState.Modified);
            }

            if (this.LeftDocument.Name != this.RightDocument.Name)
            {
                yield return new DiffChange("Document.Name", VsonDiffState.Modified);
            }

            if (this.LeftDocument.CanvasLocation != this.RightDocument.CanvasLocation)
            {
                yield return new DiffChange("Document.CanvasLocation", VsonDiffState.Modified);
            }

            if (this.LeftDocument.CanvasZoom != this.RightDocument.CanvasZoom)
            {
                yield return new DiffChange("Document.CanvasZoom", VsonDiffState.Modified);
            }
        }

        public IEnumerable<DiffChange> CompareComponents()
        {
            if (this.LeftDocument.Components.Count != this.RightDocument.Components.Count)
            {
                yield return new DiffChange("ComponentCount", VsonDiffState.Modified);

            }

            HashSet<VsonComponent> visited = new HashSet<VsonComponent>();

            foreach (VsonComponent component in this.LeftDocument.Components)
            {
                if (this.RightDocument.ComponentIDs.Contains(component.ComponentGuid) == false)
                {
                    string description = $"{component.Name} Component ({component.InstanceGuid}) was deleted.";
                    yield return new DiffChange($"{description}", VsonDiffState.Removed);
                }
                else
                {
                    // Diff the component here
                }
            }

            foreach (VsonComponent component in this.RightDocument.Components)
            {
                if (this.LeftDocument.ComponentIDs.Contains(component.ComponentGuid) == false)
                {
                    string description = $"{component.Name} Component ({component.InstanceGuid}) was added.";
                    yield return new DiffChange($"{description}", VsonDiffState.Added);

                }
                else
                {
                    //WARNING: REDUNDANT SOMETIMES
                    
                    // Diff the component here
                }
            }
        }
        #endregion Methods

        /* THINGS TO CHECK
         * 1. 
         */
    }
}
