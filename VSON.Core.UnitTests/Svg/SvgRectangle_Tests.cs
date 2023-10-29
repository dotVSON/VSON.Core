namespace VSON.Core.Svg.Tests
{
    public class SvgRectangle_Tests
    {
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
            Assert.Equal(expected, result);
        }
    }
}
