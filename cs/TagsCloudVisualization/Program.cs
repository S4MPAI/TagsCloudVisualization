using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudVisualization;

public static class Program
{
    public const int ImageWidth = 1920;
    public const int ImageHeight = 1080;
    public const int CountRectangles = 1000;
    
    public static void Main()
    {
        var center = new Point(ImageWidth / 2, ImageHeight / 2);
        var cloudLayouter = new CircularCloudLayouter(center);
        var random = new Random();
        var rectangles = new Rectangle[CountRectangles];
        
        for (int i = 0; i < CountRectangles; i++)
        {
            var size =new Size(random.Next(10, 50), random.Next(10, 50));
            rectangles[i] = cloudLayouter.PutNextRectangle(size);
        }

        var visualizer = new DefaultVisualizer();
        var bitmap = visualizer.CreateBitmap(rectangles, new Size(ImageWidth, ImageHeight));
        var fileName = $"{CountRectangles}TagsCloud.png";
        
        bitmap.Save(fileName);
    }
}