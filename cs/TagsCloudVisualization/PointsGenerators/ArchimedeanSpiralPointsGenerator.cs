using System.Drawing;

namespace TagsCloudVisualization.PointsGenerators;

public class ArchimedeanSpiralPointsGenerator : IPointsGenerator
{
    private readonly double offsetPerRadian;
    private readonly double angleOffset;
    
    public ArchimedeanSpiralPointsGenerator(double radius, double angleOffset)
    {
        if (radius <= 0)
            throw new ArgumentException("radius must be greater than 0", nameof(radius));
        if (angleOffset == 0)
            throw new ArgumentException("angleOffset must not be 0", nameof(angleOffset));
        
        offsetPerRadian = radius / (2 * Math.PI);
        this.angleOffset = angleOffset * Math.PI / 180;
    }
    
    public IEnumerable<Point> GeneratePoints(Point startPoint)
    {
        var angle = 0d;
        
        while (true)
        {
            yield return ConvertFromPolarCoordinates(startPoint, angle);
            
            angle += angleOffset;
        }
    }

    private Point ConvertFromPolarCoordinates(Point startPoint, double angle)
    {
        var p = offsetPerRadian * angle;
        var x = (int)Math.Round(startPoint.X + p * Math.Cos(angle));
        var y = (int)Math.Round(startPoint.Y + p * Math.Sin(angle));
        
        return new Point(x, y);
    }
}