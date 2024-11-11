using System.Drawing;

namespace TagsCloudVisualization.Base;

public static class PolarMath
{
    public static double ConvertToRadians(double degrees) =>
        degrees * Math.PI / 180;

    public static double GetOffsetPerRadianForArchimedeanSpiral(double radius) =>
        radius / (2 * Math.PI);

    public static Point ConvertToCartesianCoordinateSystem(double polarRadius, double polarAngle)
    {
        var x = (int)Math.Round(polarRadius * Math.Cos(polarAngle));
        var y = (int)Math.Round(polarRadius * Math.Sin(polarAngle));
        
        return new Point(x, y);
    }
}