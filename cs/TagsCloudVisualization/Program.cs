using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudVisualization;

public static class Program
{
    public const int ImageWidth = 1600;
    public const int ImageHeight = 1600;
    public const int CountRectangles = 1000;
    public const int minLength = 10;
    public const int maxLength = 20;
    
    public static void Main()
    {
        var center = new Point(ImageWidth / 2, ImageHeight / 2);
        var cloudLayouter = new CircularCloudLayouter(center);
        var random = new Random();
        var rectangles = new Rectangle[CountRectangles];
        
        for (int i = 0; i < CountRectangles; i++)
        {
            var size = random.NextSize(minLength, maxLength);
            rectangles[i] = cloudLayouter.PutNextRectangle(size);
        }

        var visualizer = new DefaultVisualizer();
        var bitmap = visualizer.CreateBitmap(rectangles, new Size(ImageWidth, ImageHeight));
        var path = GetPathToTagsCloudImage();

        bitmap.Save(path);
    }

    private static string GetPathToTagsCloudImage()
    {
        var fileName = $"{CountRectangles}_{minLength}_{maxLength}_TagsCloud.png";
        var imagesDirectory = "images";
        Directory.CreateDirectory(imagesDirectory);
        
        return $"{imagesDirectory}\\{fileName}";
    }
}