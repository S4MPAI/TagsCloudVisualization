using System.Drawing;

namespace TagsCloudVisualization.Visualizers;

public class DefaultVisualizer : IVisualizer
{
    private const int MinColorComponentValue = 0;
    private const int MaxColorComponentValue = 255;
    private readonly Random random = new();
    
    public Bitmap CreateBitmap(IList<Rectangle> rectangles, Size bitmapSize)
    {
        var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
        var graphics = Graphics.FromImage(bitmap);
        
        foreach (var rectangle in rectangles)
        {
            var pen = GetRandomPen();
            graphics.DrawRectangle(pen, rectangle);
        }
        
        return bitmap;
    }

    private Pen GetRandomPen()
    {
        var color = Color.FromArgb
        (
            random.Next(MinColorComponentValue, MaxColorComponentValue), 
            random.Next(MinColorComponentValue, MaxColorComponentValue), 
            random.Next(MinColorComponentValue, MaxColorComponentValue)
        );
        
        return new Pen(color);
    }
}