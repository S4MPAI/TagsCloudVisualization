using System.Drawing;

namespace TagsCloudVisualization.Visualizers;

public class DefaultVisualizer : IVisualizer
{
    private const int minColorComponentValue = 0;
    private const int maxColorComponentValue = 255;
    
    public Bitmap CreateBitmap(IList<Rectangle> rectangles, Size bitmapSize)
    {
        var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
        var graphics = Graphics.FromImage(bitmap);
        var random = new Random();
        
        foreach (var rectangle in rectangles)
        {
            var pen = GetRandomPen(random);
            graphics.DrawRectangle(pen, rectangle);
        }
        
        return bitmap;
    }

    private static Pen GetRandomPen(Random random)
    {
        var color = Color.FromArgb
        (
            random.Next(minColorComponentValue, maxColorComponentValue), 
            random.Next(minColorComponentValue, maxColorComponentValue), 
            random.Next(minColorComponentValue, maxColorComponentValue)
        );
        
        return new Pen(color);
    }
}