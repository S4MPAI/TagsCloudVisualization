using System.Drawing;

namespace TagsCloudVisualization.CloudLayouters;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly Point center;
    private IEnumerator<Point> points;
    private List<Rectangle> rectangles = [];

    public CircularCloudLayouter(Point center)
    {
        this.center = center;
        var pointsGenerator = new ArchimedeanSpiralPointsGenerator(2, 0.5);
        points = pointsGenerator.GeneratePoints(center).GetEnumerator();
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        Rectangle rectangle;
        do
        {
            points.MoveNext();
            var rectanglePos = points.Current;
            rectangle = CreateRectangleWithCenter(rectanglePos, rectangleSize);
        } while (rectangles.Any(rectangle.IntersectsWith));
        rectangles.Add(rectangle);
        
        return rectangle;
    }

    private Rectangle CreateRectangleWithCenter(Point centerPos, Size rectangleSize)
    {
        var xPos = centerPos.X - rectangleSize.Width / 2;
        var yPos = centerPos.Y - rectangleSize.Height / 2;
            
        return new Rectangle(xPos, yPos, rectangleSize.Width, rectangleSize.Height);
    }
}