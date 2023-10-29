namespace VSON.Core.Svg.Tests
{
    public class SvgCircle_Tests
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
    }
}
