using Newtonsoft.Json;
using System;
using System.Drawing;

namespace VSON.Core
{    
    [Serializable]
    public class CanvasElement
    {
        #region Properties
        public string Discriminator { get; set; }

        [JsonIgnore] public Document ActiveDocument { get; set; }

        public Guid InstanceGuid { get; set; }

        public virtual RectangleF Bounds { get; set; }
        #endregion Properties

        #region Methods
        public virtual string DrawSVG() => throw new NotImplementedException();

        public virtual string Serialize() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion Methods
    }
}
