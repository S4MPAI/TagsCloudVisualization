using System.Drawing;

namespace TagsCloudVisualization.Base;

public class RectanglesWindow
{
    public Point Position { get; private set; } = new(int.MaxValue, int.MaxValue);
    public int Width => end.X - Position.X;
    public int Height => end.Y - Position.Y;
    public Point Center => new(Position.X + Width / 2, Position.Y + Height / 2);
    private Point end = new(int.MinValue, int.MinValue);

    public RectanglesWindow(IEnumerable<Rectangle> rectangles)
    {
        PutRectangles(rectangles);
    }

    private void PutRectangles(IEnumerable<Rectangle> rectangles)
    {
        foreach (var rectangle in rectangles)
            PutRectangle(rectangle);
    }

    private void PutRectangle(Rectangle rectangle)
    {
        Position = new Point
        {
            X = Math.Min(rectangle.Left, Position.X),
            Y = Math.Min(rectangle.Top, Position.Y)
        };
        
        end.X = Math.Max(rectangle.Right, end.X);
        end.Y = Math.Max(rectangle.Bottom, end.Y);
    }
}