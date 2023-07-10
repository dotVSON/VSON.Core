using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VisualDiff_GH.Engine
{

    [Serializable]
    public class DiffFile
    {
        [JsonConstructor]
        private DiffFile() { }

        public DiffFile(List<Component> components)
        {
            this.Components = components;
        }

        [JsonProperty("Components")]
        public List<Component> Components { get; set; } = new List<Component>();
    }

    public class Comparer
    {
        public static List<Component>[] CompareJson(string leftFile, string rightFile)
        {
            /*DiffFile
                leftDiffFile = JsonConvert.DeserializeObject<DiffFile>(leftFile),
                rightDiffFile = JsonConvert.DeserializeObject<DiffFile>(rightFile);*/

            IEnumerable<Component>
                leftComponents = JsonConvert.DeserializeObject<Component[]>(leftFile),
                rightComponents = JsonConvert.DeserializeObject<Component[]>(rightFile);

            Dictionary<Guid, Component>
                left = new Dictionary<Guid, Component>(),
                right = new Dictionary<Guid, Component>(),
                removed = new Dictionary<Guid, Component>(),
                added = new Dictionary<Guid, Component>(),
                retained = new Dictionary<Guid, Component>();

            foreach (Component comp in leftComponents)
            {
                left.Add(comp.InstanceGuid, comp);
            }

            foreach (Component comp in rightComponents)
            {
                right.Add(comp.InstanceGuid, comp);
            }

            // Check If Components are Removed
            foreach (KeyValuePair<Guid, Component> pair in left)
            {
                if (right.ContainsKey(pair.Key))
                {
                    retained.Add(pair.Key, pair.Value);
                }
                else
                {
                    removed.Add(pair.Key, pair.Value);
                }
            }

            // Check if Components are Added
            foreach (KeyValuePair<Guid, Component> pair in right)
            {
                if (left.ContainsKey(pair.Key) == false)
                {
                    added.Add(pair.Key, pair.Value);
                }
            }

            // Filter others


            List<Component>[] output = new List<Component>[3];
            output[0] = added.Values.ToList();
            output[1] = removed.Values.ToList();
            output[2] = retained.Values.ToList();

            return output;
        }
    }
}
