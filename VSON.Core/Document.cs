using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSON.Core
{
    /// <summary>
    /// An Isolated collection of components that are connected.
    /// 
    /// A Document usually has one graph. Multiple graphs are also common.
    /// </summary>
    [Serializable]
    public class Document
    {
        #region Constructors
        [JsonConstructor]
        private Document() { }

        public Document(string filePath) : this()
        {

        }
        #endregion Constructors

        #region Properties
        

        
        #endregion Properties

        #region Methods

        #endregion Methods
    }
}
