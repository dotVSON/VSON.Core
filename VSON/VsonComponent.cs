using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VSON
{
    [Serializable]
    public class VsonComponent
    {
        #region Properties
        public virtual VsonComponentType ComponentType { get; set; }
        public virtual string Type { get; set; }
        public virtual Guid ComponentGuid { get; set    ; }
        public virtual Guid InstanceGuid { get; set; }
        public virtual string Name { get; set; }
        public virtual string NickName { get; set; }
        public virtual string Message { get; set; }
        public bool Hidden { get; set; }
        public bool Locked { get; set; }
        public virtual PointF Pivot { get; set; }
        public virtual RectangleF Bounds { get; set; }
        public virtual SizeF Size { get => new SizeF(this.Bounds.Width, this.Bounds.Height);}
        public virtual List<VsonComponent> InputParams { get; set; }
        public virtual List<VsonComponent> OutputParams { get; set; }
        #endregion Properties

        #region Methods
        public static T Deserialze<T>(string text) where T : VsonComponent
        {
            return JsonConvert.DeserializeObject<T>(text);
        }

        public static T DeserializeFromFile<T>(string path) where T: VsonComponent
        {
            if (File.Exists(path))
            {
                string contents = File.ReadAllText(path);
                return Deserialze<T>(contents);
            }
            throw new FileNotFoundException(path);
        }

        public static JObject DeserializeToJObject(string text)
        {
            return JsonConvert.DeserializeObject(text) as JObject;
        }

        public static JToken DeserializeToJToken(string text, string key = "ComponentType")
        {
            return VsonComponent.DeserializeToJObject(text)[text] as JToken;
        }

        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion Methods
    }
}
