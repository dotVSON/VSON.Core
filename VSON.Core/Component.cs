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
    [Serializable]
    public class VsonComponentAttributes
    {

    }


    [Serializable]
    public class Component
    {
        #region Fields
        private static List<ComponentType> parentComponentTypes = new List<ComponentType> { ComponentType.GrasshopperComponent };
        private static List<ComponentType> childComponentTypes = new List<ComponentType> { ComponentType.GrasshopperParam };
        #endregion Fields

        #region Properties
        public virtual ComponentType ComponentType { get; set; }
        
        public virtual string Type { get; set; }
        
        public virtual Guid ComponentGuid { get; set; }
        
        public virtual Guid InstanceGuid { get; set; }
        
        public virtual string Name { get; set; }
        
        public virtual string NickName { get; set; }
        
        public virtual string Message { get; set; }
        
        public bool Hidden { get; set; }
        
        public bool Locked { get; set; }

        public bool IsParam { get => Component.childComponentTypes.Contains(this.ComponentType); }

        public bool IsComponent { get => Component.parentComponentTypes.Contains(this.ComponentType); }
        
        public virtual PointF Pivot { get; set; }
        
        public virtual RectangleF Bounds { get; set; }
        
        public virtual SizeF Size { get => new SizeF(this.Bounds.Width, this.Bounds.Height);}
        
        public virtual List<Component> InputParams { get; set; }
        
        public virtual List<Component> OutputParams { get; set; }

        public virtual List<Guid> SourceParams { get; set; }

        public virtual List<Guid> DestinationParams { get; set; }
        #endregion Properties

        #region Methods
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

        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static string DrawComponent(Component component)
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
            SvgRectangle componentRectangle = new SvgRectangle(component.Bounds, componentStyle)
            {
                xRadius = 5,
                yRadius = 5,
            };
            svg.AppendLine(componentRectangle.ToXML());

            // Draw Param Circles
            SvgStyle paramStyle = new SvgStyle()
            {
                Stroke = "black",
                StrokeWidth = 2,
                Fill = "white",
            };
            
            foreach (Component param in component.InputParams)
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

            foreach (Component param in component.OutputParams)
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
                X = component.Bounds.X + inputParamWidth,
                Y = component.Bounds.Y,
                Width = component.Bounds.Width - (inputParamWidth + outputParamWidth),
                Height = component.Bounds.Height,
                Style = nameStripStyle,
                xRadius = 3,
                yRadius = 3,
            };
            svg.AppendLine(nameStripRectangle.ToXML());

            return svg.ToString();
        }

        public static string DrawComponent(string json) => DrawComponent(Deserialze<Component>(json));

        public void Test()
        {
            foreach (Component param in this.InputParams)
            {
                
            }
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
