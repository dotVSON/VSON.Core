using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using VSON.Core.Svg;

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
        public static string GetValueFromConfig(string key)
        {
            Console.WriteLine("Hello");
            Console.WriteLine("Bye");
            
            return ConfigurationManager.AppSettings[key];
        }
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
        
        public virtual void LoadFromDocument(string path, object document = null)
        {
            throw new NotImplementedException();
        }

        public void Register(Parameter parameter)
        {
            parameter.ActiveDocument = this;
            if (this.ParameterTable.ContainsKey(parameter.InstanceGuid) == false)
            {
                this.ParameterTable.Add(parameter.InstanceGuid, parameter);
            }
        }
        
        public void Register(Component component)
        {
            component.ActiveDocument = this;
            if (this.ComponentTable.ContainsKey(component.InstanceGuid) == false)
            {
                this.ComponentTable.Add(component.InstanceGuid, component);
            }
        }

        public RectangleF CanvasBounds()
        {
            RectangleF canvasBounds = RectangleF.Empty;
            foreach (Component component in this.ComponentTable.Values)
            {
                canvasBounds = RectangleF.Union(canvasBounds, component.Bounds);
            }
            return canvasBounds;
        }

        public void Register(Wire wire)
        {
            wire.ActiveDocument = this;
            if (this.WireTable.ContainsKey(wire.InstanceGuid) == false)
            {
                this.WireTable.Add(wire.InstanceGuid, wire);
            }
        }

        public RectangleF GetCanvasBounds(float inflate = 50)
        {
            RectangleF canvasBounds = RectangleF.Empty;
            foreach (Component component in this.ComponentTable.Values)
            {
                canvasBounds = RectangleF.Union(canvasBounds, component.Bounds);
            }
            canvasBounds.Inflate(inflate, inflate);
            return canvasBounds;
        }

        public StringBuilder DrawCanvas()
        {
            StringBuilder svg = new StringBuilder();

            RectangleF canvasBounds = this.GetCanvasBounds();
            string title =
                $" <svg" +
                $" xmlns=\"http://www.w3.org/2000/svg\"" +
                $" width=\"100%\" height=\"100%\"" +
                $" viewBox=\"{canvasBounds.Left} {canvasBounds.Top} {canvasBounds.Width} {canvasBounds.Height}\"" +
                $" fill=\"grey\" >";
            svg.AppendLine(title);

            SvgStyle canvasStyle = new SvgStyle()
            {
                Stroke = "black",
                StrokeWidth = 0.5,
                Fill = "white"
            };
            SvgRectangle canvasRectangle = new SvgRectangle(canvasBounds, canvasStyle);

            SvgStyle xStyle = new SvgStyle()
            {
                Fill = "none",
                Stroke = "red",
                StrokeWidth = 2,
            };
            SvgLine XAxis = new SvgLine()
            {
                X1 = 0,
                Y1 = 0,
                X2 = canvasBounds.Right - canvasBounds.Left,
                Y2 = 0,
                Style = xStyle,
            };

            SvgStyle yStyle = new SvgStyle()
            {
                Fill = "none",
                Stroke = "green",
                StrokeWidth = 2,
            };
            SvgLine YAxis = new SvgLine()
            {
                X1 = 0,
                Y1 = 0,
                X2 = 0,
                Y2 = canvasBounds.Bottom - canvasBounds.Top,
                Style = yStyle,
            };

            SvgStyle originStyle = new SvgStyle()
            {
                Fill = "black",
                Stroke = "none",
                StrokeWidth = 0,
            };
            SvgCircle originPoint = new SvgCircle(0, 0, 5)
            {
                Style = originStyle,
            };

            svg.AppendLine(canvasRectangle.ToXML());
            svg.AppendLine(XAxis.ToXML());
            svg.AppendLine(YAxis.ToXML());
            svg.AppendLine(originPoint.ToXML());

            return svg;
        }

        public void DrawWires(ref StringBuilder svg)
        {
            foreach (Wire wire in this.WireTable.Values)
            {
                svg.AppendLine(wire.DrawSVG());
            }
        }

        public void DrawComponents(ref StringBuilder svg)
        {
            foreach (Component component in this.ComponentTable.Values)
            {
                svg.AppendLine(component.DrawSVG());
            }
        }

        public void DrawParameters(ref StringBuilder svg)
        {
            foreach (Parameter parameter in this.ParameterTable.Values)
            {
                svg.AppendLine(parameter.DrawSVG());
            }
        }

        public override string DrawSVG()
        {
            StringBuilder svg = this.DrawCanvas();

            // Draw Wires
            this.DrawWires(ref svg);
            // Draw Components
            this.DrawComponents(ref svg);
            // Draw Parameters
            //this.DrawParameters(ref svg);

            svg.AppendLine("</svg>");

            return svg.ToString();
        }
        
        public void ExportSVG()
        {
            if (File.Exists(this.FilePath))
            {
                string
                    content = this.DrawSVG(),
                    path = Path.ChangeExtension(this.FilePath, "svg");
                File.WriteAllText(path, content);
            }
            else
            {
                throw new FileNotFoundException(this.FilePath);
            }
        }
        #endregion Methods
    }
}
