using System.Drawing;

namespace TagsCloudVisualization.Visualizers;

[SupportedOSPlatform("windows")]
public class DefaultVisualizer(Point offset) : IVisualizer
{
    private const int MinColorComponentValue = 0;
    private const int MaxColorComponentValue = 255;
    private readonly Random random = new();

    public DefaultVisualizer() : this(new Point())
    {
    }
    
    public Bitmap CreateBitmap(IEnumerable<Rectangle> rectangles, Size bitmapSize)
    {
        var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
        using var graphics = Graphics.FromImage(bitmap);
        
        graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(), bitmapSize));
        foreach (var rectangle in rectangles)
        {
            rectangle.Offset(offset);
            graphics.DrawRectangle(GetRandomPen(), rectangle);
        }
        
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