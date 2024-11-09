using System.Drawing;
using TagsCloudVisualization.PointsGenerators;

namespace TagsCloudVisualization;

public class ArchimedeanSpiralPointsGenerator : IPointsGenerator
{
    private readonly double offsetPerRadian;
    private readonly double angleOffset;
    
    public ArchimedeanSpiralPointsGenerator(double radius, double angleOffset)
    {
        offsetPerRadian = radius / (2 * Math.PI);
        this.angleOffset = angleOffset * Math.PI / 180;
    }
    
    public IEnumerable<Point> GeneratePoints(Point startPoint)
    {
        var angle = 0d;
        
        while (true)
        {
            var p = offsetPerRadian * angle;
            var x = (int)Math.Ceiling(startPoint.X + p * Math.Cos(angle));
            var y = (int)Math.Ceiling(startPoint.Y + p * Math.Sin(angle));
            
            yield return new Point(x, y);
            
            angle += angleOffset;
        }
    }
}