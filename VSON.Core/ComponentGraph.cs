using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VSON.Core
{
    /// <summary>
    /// An Isolated collection of components that are connected.
    /// </summary>
    [Serializable]
    public class ComponentGraph
    {
        #region Constructors
        [JsonConstructor]
        private ComponentGraph() { }

        public ComponentGraph(string filePath) : this()
        {

        }
        #endregion Constructors

        #region Properties
        public Dictionary<Guid, Component> ComponentTable { get; private set; }
        
        public Dictionary<Guid, Component> ParamTable { get; private set; }

        public Dictionary<Guid, Wire> WireTable { get; private set; }

        
        #endregion Properties

        #region Methods

        #endregion Methods
    }
}
