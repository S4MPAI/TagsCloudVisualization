using System.Drawing;

namespace TagsCloudVisualization.Base;

public static class PolarMath
{
    public static double ConvertToRadians(double degrees) =>
        degrees * Math.PI / 180;

    public static double ConvertToDegrees(double radians) =>
        radians * 180 / Math.PI;

    public static double GetSquareOfCircle(double radius) => 
        Math.PI * radius * radius;
    
    public static double GetOffsetPerRadianForArchimedeanSpiral(double radius) =>
        radius / (2 * Math.PI);

    public static Point ConvertToCartesianCoordinateSystem(double polarRadius, double polarAngle)
    {
        var x = (int)Math.Round(polarRadius * Math.Cos(polarAngle));
        var y = (int)Math.Round(polarRadius * Math.Sin(polarAngle));
        
        return new Point(x, y);
    }
    
    public static (double polarRadius, double polarAngle) ConvertToPolarCoordinateSystem(Point point)
    {
        var polarRadius = Math.Sqrt(point.X * point.X + point.Y * point.Y);
        var polarAngle = Math.Atan2(point.Y, point.X);
        polarAngle = polarAngle < 0 ? 2 * Math.PI + polarAngle : polarAngle;
        
        return (polarRadius, polarAngle);
    }
}