using System.Drawing;

namespace TagsCloudVisualization.Visualizers;

public interface IVisualizer
{
    public Bitmap CreateBitmap(IList<Rectangle> rectangles, Size bitmapSize);
}