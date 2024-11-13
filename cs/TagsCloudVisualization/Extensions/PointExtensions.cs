using System.Drawing;

namespace TagsCloudVisualization.Extensions;

public static class PointExtensions
{
    public static Point Subtract(this Point p1, Point p2) => 
        new(p1.X - p2.X, p1.Y - p2.Y);
    
    public static Point Abs(this Point p) => 
        new(Math.Abs(p.X), Math.Abs(p.Y));
}