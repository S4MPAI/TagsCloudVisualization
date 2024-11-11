using System.Drawing;
using System.Runtime.Versioning;

namespace TagsCloudVisualization.Visualizers;

[SupportedOSPlatform("windows")]
public class DefaultVisualizer : IVisualizer
{
    private const int MinColorComponentValue = 0;
    private const int MaxColorComponentValue = 255;
    private readonly Random random = new();
    
    public Bitmap CreateBitmap(IEnumerable<Rectangle> rectangles, Size bitmapSize)
    {
        var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
        using var graphics = Graphics.FromImage(bitmap);
        
        foreach (var rectangle in rectangles)
            graphics.DrawRectangle(GetRandomPen(), rectangle);
        
        return bitmap;
    }

    private Pen GetRandomPen() =>
        new
        (Color.FromArgb(
            GetRandomArgbColorComponent(),
            GetRandomArgbColorComponent(),
            GetRandomArgbColorComponent())
        );

    private int GetRandomArgbColorComponent() => 
        random.Next(MinColorComponentValue, MaxColorComponentValue);
}