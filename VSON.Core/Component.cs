using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using VSON.Core.Svg;

namespace VSON.Core
{
    public enum ComponentType
    {
        MissingComponent = -1,
        GenericComponent = 0,
        GrasshopperParam = 1,
        GrasshopperComponent = 2,
        GrasshopperSpecialParam = 3,
    }

    [Serializable]
    public class Component : CanvasElement, IDiff
    {
        #region Constructor
        public Component()
        {
            this.InputParameters = new List<Parameter>();
            this.OutputParameters = new List<Parameter>();
        }
        #endregion Constructor

        #region Properties
        public ComponentType ComponentType { get; set; }

        public DiffState DiffState { get; set; } = DiffState.Unknown;

        public Guid ComponentGuid { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }

        public string Message { get; set; }

        public bool IsHidden { get; set; }

        public bool IsLocked { get; set; }

        public bool IsSpecial { get; set; }

        public virtual PointF Pivot { get; set; }

        public virtual SizeF Size { get => new SizeF(this.Bounds.Width, this.Bounds.Height); }

        public List<Parameter> InputParameters { get; set; }

        public List<Parameter> OutputParameters { get; set; }
        #endregion Properties

        #region DeSerialization
        public static T Deserialze<T>(string text) where T : Component
        {
            return JsonConvert.DeserializeObject<T>(text);
        }

        public static T DeserializeFromFile<T>(string path) where T: Component
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
            return Component.DeserializeToJObject(text)[text] as JToken;
        }

        #endregion DeSerialization

        #region Methods

        public override string DrawSVG()
        {
            double
                paramCircleRadius = 4,
                inputParamWidth = 0,
                outputParamWidth = 0;
            StringBuilder svg = new StringBuilder();

            // Draw Component Rectangle
            SvgStyle componentStyle = new SvgStyle()
            {
                Fill = "#F0F0F0",
                Stroke = "black",
                StrokeWidth = 2,
            };
            SvgRectangle componentRectangle = new SvgRectangle(this.Bounds, componentStyle)
            {
                XRadius = 5,
                YRadius = 5,
            };
            svg.AppendLine(componentRectangle.ToXML());

            // Draw Param Circles
            SvgStyle paramStyle = new SvgStyle()
            {
                Stroke = "black",
                StrokeWidth = 2,
                Fill = "white",
            };
            
            foreach (Parameter param in this.InputParameters)
            {
                if (inputParamWidth == 0)
                {
                    inputParamWidth = param.Bounds.Width;
                }

                SvgCircle paramCircle = new SvgCircle()
                {
                    X = param.Bounds.X-paramStyle.StrokeWidth,
                    Y = param.Bounds.Bottom - (param.Bounds.Height * 0.5), 
                    Radius = paramCircleRadius, 
                    Style = paramStyle,
                };
                svg.AppendLine(paramCircle.ToXML());
            }

            foreach (Parameter param in this.OutputParameters)
            {
                if (outputParamWidth == 0)
                {
                    outputParamWidth = param.Bounds.Width;
                }

                SvgCircle paramCircle = new SvgCircle()
                {
                    X = param.Bounds.Right + paramStyle.StrokeWidth,
                    Y = param.Bounds.Bottom - param.Bounds.Height * 0.5,
                    Radius = paramCircleRadius,
                    Style = paramStyle,
                };
                svg.AppendLine(paramCircle.ToXML());
            }

            // Draw Component Icon / Name Strip
            SvgStyle nameStripStyle = new SvgStyle()
            {
                Fill = "#4D4D4D",
                Stroke = "black",
                StrokeWidth = 2,
            };
            SvgRectangle nameStripRectangle = new SvgRectangle()
            {
                X = this.Bounds.X + inputParamWidth,
                Y = this.Bounds.Y,
                Width = this.Bounds.Width - (inputParamWidth + outputParamWidth),
                Height = this.Bounds.Height,
                Style = nameStripStyle,
                XRadius = 3,
                YRadius = 3,
            };
            svg.AppendLine(nameStripRectangle.ToXML());

            return svg.ToString();
        }
        #endregion Methods

        #region DiffMethods
        public string GetHashString()
        {
            StringBuilder hash = new StringBuilder();
            foreach (System.Reflection.PropertyInfo property in this.GetType().GetProperties())
            {
                hash.Append(property.GetValue(this));
            }
            return hash.ToString();
        }

        public static bool QuickCheck(Component componentA, Component componentB)
        {
            return componentA.GetHashString().Equals(componentB.GetHashString());
        }
        
        public static string DeepCheck(Component componentA, Component componentB)
        {
            StringBuilder status = new StringBuilder();

            foreach (System.Reflection.PropertyInfo property in componentA.GetType().GetProperties())
            {
                var valA = property.GetValue(componentA);
                var valB = property.GetValue(componentB);

                if (valA == null && valB != null)
                {
                    status.AppendLine($"[A] : {property.Name}");
                }
                else if (valA != null && valB == null)
                {
                    status.AppendLine($"[R] : {property.Name}");
                }
                
                else if (property.GetValue(componentA) != property.GetValue(componentB))
                {
                    status.AppendLine($"[M] : {property.Name}");
                }
            }

            return status.ToString();
        }
        #endregion DiffMethods
    }
}
