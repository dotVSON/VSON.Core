using Grasshopper;
using Grasshopper.Kernel;

namespace VSON.Grasshopper
{
    public class AssemblyLoader : GH_AssemblyPriority
    {
        public override GH_LoadingInstruction PriorityLoad()
        {
            Instances.DocumentServer.DocumentAdded -= OnDocumentAdded;
            Instances.DocumentServer.DocumentAdded += OnDocumentAdded;

            Instances.DocumentServer.DocumentRemoved -= OnDocumentRemoved;
            Instances.DocumentServer.DocumentRemoved += OnDocumentRemoved;

            return GH_LoadingInstruction.Proceed;
        }

        private static void OnDocumentAdded(object sender, GH_Document document)
        {
            document.ModifiedChanged -= OnModifiedChanged;
            document.ModifiedChanged += OnModifiedChanged;
        }

        private static void OnDocumentRemoved(object sender, GH_Document document)
        {
            document.ModifiedChanged -= OnModifiedChanged;
        }

        private static void OnModifiedChanged(object sender, GH_DocModifiedEventArgs args)
        {
            // Save only when "Modified" tag is remvoed. A.K.A File is saved.
            if (args.Modified == false)
            {
                GH_AbstractDocument vsonDoc = new GH_AbstractDocument(args.Document);
                //vsonDoc.Save();
            }
        }
    }
}
