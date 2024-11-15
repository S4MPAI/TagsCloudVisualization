using System.Drawing;
using System.Runtime.Versioning;
using TagsCloudVisualization.Base;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudVisualization;

public static class Program
{
    private const int CountRectangles = 100;
    private const int MinLength = 30;
    private const int MaxLength = 80;
    
    public static void Main()
    {
        var cloudLayouter = new CircularCloudLayouter(new Point(), 1, 0.1);
        var random = new Random();
        var rectangles = Enumerable
            .Range(0, CountRectangles)
            .Select(x => random.NextSize(MinLength, MaxLength))
            .Select(size => cloudLayouter.PutNextRectangle(size))
            .ToList();

        var rectanglesWindow = new RectanglesWindow(rectangles);
        var center = new Point(rectanglesWindow.Width / 2, rectanglesWindow.Height / 2);
        var visualizer = new DefaultVisualizer(center);
        using var bitmap = visualizer.CreateBitmap(rectangles, new Size(rectanglesWindow.Width, rectanglesWindow.Height));
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