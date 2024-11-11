using System.Drawing;
using TagsCloudVisualization.Base;

namespace TagsCloudVisualization.PointsGenerators;

public class ArchimedeanSpiralPointsGenerator : IPointsGenerator
{
    private readonly double offsetPerRadian;
    private readonly double radiansAngleOffset;
    
    public ArchimedeanSpiralPointsGenerator(double radius, double angleOffset)
    {
        if (radius <= 0)
            throw new ArgumentException("radius must be greater than 0", nameof(radius));
        if (angleOffset == 0)
            throw new ArgumentException("angleOffset must not be 0", nameof(angleOffset));
        
        offsetPerRadian = PolarMath.GetOffsetPerRadianForArchimedeanSpiral(radius);
        radiansAngleOffset = PolarMath.ConvertToRadians(angleOffset);
    }
    
    public IEnumerable<Point> GeneratePoints(Point startPoint)
    {
        var radiansAngle = 0d;
        
        while (true)
        {
            var polarRadius = offsetPerRadian * radiansAngle;
            var pointOnSpiral = PolarMath.ConvertToCartesianCoordinateSystem(polarRadius, radiansAngle);
            pointOnSpiral.Offset(startPoint);
            
            yield return pointOnSpiral;
            
            radiansAngle += radiansAngleOffset;
        }
    }
}