using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace VSON.Core
{
    /// <summary>
    /// An Isolated collection of components that are connected.
    /// </summary>
    [Serializable]
    public class Document : CanvasElement, IDiff
    {
        #region Constructors
        [JsonConstructor]
        protected Document()
        {
            this.ActiveDocument = this;
            this.ParameterTable = new Dictionary<Guid, Parameter>();
            this.WireTable = new Dictionary<Guid, Wire>();
            this.ComponentTable = new Dictionary<Guid, Component>();
        }
        #endregion Constructors

        #region Properties
        
        public DiffState DiffState { get; set; } = DiffState.Unknown;

        public  string Name { get; set; }

        public Guid Id { get => this.InstanceGuid; }

        public string FilePath { get; set; }

        public SizeF CanvasLocation { get; set; }

        public float CanvasZoom { get; set; }

        //Dictionary<string, Vertex> VertexTable; // Param Table
        public Dictionary<Guid, Parameter> ParameterTable { get; set; }

        //Dictionary<string, Edge> EdgeTable; // Wire Table
        public Dictionary<Guid, Wire> WireTable { get; set; }

        //Dictionary<string, Face> FaceTable; // Component Table
        public Dictionary<Guid, Component> ComponentTable { get; set; }

        #endregion Properties

        #region Methods
        public string SaveAsDocument()
        {
            if (File.Exists(this.FilePath))
            {
                string
                    content = this.Serialize(),
                    path = Path.ChangeExtension(this.FilePath, "vson");
                File.WriteAllText(path, content);
                
                return path;
            }
            else
            {
                throw new FileNotFoundException(this.FilePath);
            }
        }
        
        public bool SaveAsDocument(string path)
        {
            if (File.Exists(path))
            {
                string content = this.Serialize();
                File.WriteAllText(path, content);
                return true;
            }
            return false;
        }

        public void Register(Parameter parameter)
        {
            parameter.ActiveDocument = this;
            this.ParameterTable.Add(parameter.InstanceGuid, parameter);
        }

        public void Register(Component component)
        {
            component.ActiveDocument = this;
            this.ComponentTable.Add(component.InstanceGuid, component);
        }

        public void Register(Wire wire)
        {
            wire.ActiveDocument = this;
            this.WireTable.Add(wire.InstanceGuid, wire);
        }

        public string DrawCanvas()
        {
            return string.Empty;
        }

        public override string DrawSVG()
        {
            StringBuilder svg = new StringBuilder();

            // Draw Canvas Axes
            svg.AppendLine(this.DrawCanvas());
            // Draw Wires

            // Draw Components

            // Draw Parameters
            
            throw new NotImplementedException();
        }
        #endregion Methods
    }
}
