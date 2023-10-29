using System.Drawing;

namespace VSON.Core.Svg.Tests
{
    public class SvgTests
    {
        [Fact]
        public void SvgCircle_ToXml()
        {
            // Arrange
            double
                x = 50,
                y = 50,
                radius = 10,
                strokeWidth = 3;

            string
                stroke = "#000000",
                fill = "#F0F0F0";

            // Act
            SvgStyle style = new SvgStyle()
            {
                Stroke = stroke,
                Fill = fill,
                StrokeWidth = strokeWidth,
            };
            
            SvgCircle circle = new SvgCircle(x, y, radius, style);

            string expected = $" <circle cx=\"{x}\" cy=\"{y}\" r=\"{radius}\"" +                
                $" style=\" opacity: 1; fill: {fill}; fill-opacity: 1; stroke: {stroke}; stroke-opacity: 1; stroke-width: {strokeWidth};\" />";

            string result = circle.ToXML();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SvgRectangle_ToXml()
        {
            // Arrange
            double
                x = 50,
                y = 50,
                rx = 5,
                ry = 5,
                width = 100,
                height = 120,
                strokeWidth = 1;

            string
                stroke = "red",
                fill = "none";
            
            // Act
            SvgStyle style = new SvgStyle()
            {
                Fill = fill,
                Stroke = stroke,
                StrokeWidth = strokeWidth,
            };
            SvgRectangle rectangle = new SvgRectangle(x, y, width, height, style, rx, ry);

            string expected = $" <rect x=\"{x}\" y=\"{y}\" rx=\"{rx}\" ry=\"{ry}\" width=\"{width}\" height=\"{height}\" style=\" opacity: 1; fill: {fill}; fill-opacity: 1; stroke: {stroke}; stroke-opacity: 1; stroke-width: {strokeWidth};\" />";
            string result = rectangle.ToXML();

            // Assert
            Assert.Equal(expected , result);
        }

        [Fact]
        public void SvgBezierCurve_ToXml()
        {
            // Arrange
            PointF
                C0 = new PointF(123, -50),
                C1 = new PointF(369, -50),
                C2 = new PointF(69, -167),
                C3 = new PointF(315, -167);

            string
                stroke = "blue",
                fill = "none";

            double
                opacity = 0.9,
                strokeWidth = 3;

            // Act
            SvgStyle style = new SvgStyle()
            {
                Stroke = stroke,
                Fill = fill,
                StrokeWidth = strokeWidth,
                Opacity = opacity,
            };

            SvgBezierCurve curve = new SvgBezierCurve(C0, C3, style);
            string result = curve.ToXML();
            string expected = $" <path d=\" M {C0.X} {C0.Y} C {C1.X} {C1.Y}, {C2.X} {C2.Y}, {C3.X} {C3.Y}\" style=\" opacity: {opacity}; fill: {fill}; fill-opacity: 1; stroke: {stroke}; stroke-opacity: 1; stroke-width: {strokeWidth};\" />";

            // Assert
            Assert.Equal(curve.C1, C1);
            Assert.Equal(curve.C2, C2);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SvgBezierCurve_EmptyPoints_ThrowsInvalidOperationException()
        {
            // Arrange
            SvgBezierCurve curve = new SvgBezierCurve();
            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => curve.GenerateControlPoints());
        }
    }
}