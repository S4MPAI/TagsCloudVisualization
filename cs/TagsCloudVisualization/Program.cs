using System.Drawing;
using System.Runtime.Versioning;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudVisualization;

[SupportedOSPlatform("windows")]
public static class Program
{
    private const int ImageWidth = 1600;
    private const int ImageHeight = 1600;
    private const int CountRectangles = 1000;
    private const int MinLength = 10;
    private const int MaxLength = 20;
    
    public static void Main()
    {
        var center = new Point(ImageWidth / 2, ImageHeight / 2);
        var cloudLayouter = new CircularCloudLayouter(center, 1, 0.5);
        var random = new Random();
        var rectangles = Enumerable
            .Range(0, CountRectangles)
            .Select(x => random.NextSize(MinLength, MaxLength))
            .Select(size => cloudLayouter.PutNextRectangle(size));

        var visualizer = new DefaultVisualizer();
        var bitmap = visualizer.CreateBitmap(rectangles, new Size(ImageWidth, ImageHeight));
        var path = GetPathToTagsCloudImage();

        bitmap.Save(path);
    }

    private static string GetPathToTagsCloudImage()
    {
        var fileName = $"{CountRectangles}_{MinLength}_{MaxLength}_TagsCloud.png";
        var imagesDirectory = "images";
        Directory.CreateDirectory(imagesDirectory);
        
        return $"{imagesDirectory}\\{fileName}";
    }
}