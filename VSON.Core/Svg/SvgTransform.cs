using System.Collections.Generic;

namespace VSON.Core.Svg
{
    public class SvgTransform : SvgBaseElement
    {
        #region Constructor
        public SvgTransform()
        {
            this.Rotation = new Dictionary<string, double>
            {
                { "angle", 0 },
                { "x", double.NaN },
                { "y", double.NaN }
            };
        }
        #endregion Constructor

        #region Properties
        private Dictionary<string, double> Rotation { get; set; }
        #endregion Properties

        #region Methods
        public void Rotate(double Angle)
        {
            this.Rotation["angle"] = Angle;
            this.Rotation["x"] = double.NaN;
            this.Rotation["y"] = double.NaN;
        }
        public void Rotate(double Angle, double X, double Y)
        {
            this.Rotation["angle"] = Angle;
            this.Rotation["x"] = X;
            this.Rotation["y"] = Y;
        }

        private string GetRotation()
        {
            string xml = $"rotate({this.Rotation["angle"]})";

            if (this.Rotation["x"] != double.NaN && this.Rotation["y"] != double.NaN)
            {
                xml += $"{this.Rotation["x"]} {this.Rotation["y"]}";
            }
            return xml;
        }


        public override string ToXML()
        {
            throw new System.NotImplementedException();
        }
        #endregion Methods
    }
}
