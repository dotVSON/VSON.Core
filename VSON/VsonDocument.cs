using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VSON
{
    [Serializable]
    public class VsonDocument
    {
        #region Properties
        public string Name { get; set; }

        public Guid Id { get; set; }

        public SizeF CanvasLocation { get; set; }

        public float CanvasZoom { get; set; }

        public string FilePath { get; set; }

        public List<VsonComponent> Components { get; set; }
        #endregion Properties

        #region Methods
        public static T Deserialize<T>(string json) where T : VsonDocument
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T DeserializeFromFile<T>(string path) where T : VsonDocument
        {
            if (File.Exists(path))
            {
                string contents = File.ReadAllText(path);
                return Deserialize<T>(contents);
            }
            throw new FileNotFoundException(path);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public bool SerializeToFile()
        {
            if (File.Exists(this.FilePath))
            {
                string content = this.Serialize();
                string path = Path.ChangeExtension(this.FilePath, "vson");
                File.WriteAllText(path, content);
                return true;
            }
            return false;
        }

        public bool SerializeToFile(string path)
        {
            if (File.Exists(path))
            {
                string content = this.Serialize();
                File.WriteAllText(path, content);
                return true;
            }
            return false;
        }
        #endregion Methods
    }
}
