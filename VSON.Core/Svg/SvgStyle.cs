using System;

namespace VSON.Core.Svg
{
    public class SvgStyle : SvgBaseElement
    {
        #region Fields
        private double opacity = 1.0;
        private double fillOpacity = 1.0;
        private double strokeOpacity = 1.0;
        #endregion Fields

        #region Constructors
        public SvgStyle() { }

        public SvgStyle(string stroke, string fill, double strokeWidth) : this()
        {
            this.Stroke = stroke;
            this.Fill = fill;
            this.StrokeWidth = strokeWidth;
        }
        #endregion Constructors

        #region Properties
        public string Stroke { get; set; } = "black";
        
        public string Fill { get; set; } = "none";
        
        public double StrokeWidth { get; set; } = 5;

        public double Opacity
        {
            get => this.opacity;

            set
            {
                if (0 <= value && value <= 1)
                {
                    this.opacity = value;
                }
                else
                {

                    throw new ArgumentOutOfRangeException("Opacity needs to be between 0.00 and 1.00");
                }
            }
        }

        public double FillOpacity
        {
            get => this.fillOpacity;

            set
            {
                if (0 <= value && value <= 1)
                {
                    this.fillOpacity = value;
                }
                else
                {

                    throw new ArgumentOutOfRangeException("FillOpacity needs to be between 0.00 and 1.00");
                }
            }
        }

        public double StrokeOpacity
        {
            get => this.strokeOpacity;

            set
            {
                if (0 <= value && value <= 1)
                {
                    this.strokeOpacity = value;
                }
                else
                {

                    throw new ArgumentOutOfRangeException("StrokeOpacity needs to be between 0.00 and 1.00");
                }
            }
        }

        #endregion Properties

        #region Methods
        public override string ToXML()
        {
            return
                $" opacity: {this.Opacity};" +
                $" fill: {this.Fill};" +
                $" fill-opacity: {this.FillOpacity};" +
                $" stroke: {this.Stroke};" +
                $" stroke-opacity: {this.StrokeOpacity};" +
                $" stroke-width: {this.StrokeWidth};";
        }
        #endregion Methods
    }
}
