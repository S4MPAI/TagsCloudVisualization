using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using TagsCloudVisualization.Base;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudVisualizationTests;

[SupportedOSPlatform("windows")]
[TestFixture]
public class CircularCloudLayouterTests
{
    private int seed = 232445332;
    private Randomizer random;
    private Rectangle[] testRectangles;
    private const string ImagesDirectory = "testImages";
    
    [OneTimeSetUp]
    public void Init()
    {
        random = new Randomizer(seed);
    }

    [TearDown]
    public void TearDown()
    {
        var currentContext = TestContext.CurrentContext;
        if (currentContext.Result.Outcome.Status != TestStatus.Failed)
            return;

        var rectanglesWindow = new RectanglesWindow(testRectangles);
        var center = new Point(rectanglesWindow.Width / 2, rectanglesWindow.Height / 2);
        var visualizer = new DefaultVisualizer(center);
        using var bitmap = visualizer.CreateBitmap(testRectangles, new Size(rectanglesWindow.Width, rectanglesWindow.Height));
        var path = Path.Combine(ImagesDirectory, currentContext.Test.Name + ".png");
        Directory.CreateDirectory(ImagesDirectory);
        bitmap.Save(path, ImageFormat.Png);
        TestContext.Out.WriteLine($"Tag cloud visualization saved to file {path}");
    }
    
    [Test]
    [Repeat(10)]
    public void PutNextRectangle_ShouldReturnRectanglesInCenter()
    {
        var center = random.NextPoint(-100, 100);
        testRectangles = PutRectanglesInCloudLayouter(center);

        var actualRectanglesWindow = new RectanglesWindow(testRectangles);
        var centerOffset = center.Subtract(actualRectanglesWindow.Center).Abs();
        var xError = testRectangles.Max(r => r.Width);
        var yError = testRectangles.Max(r => r.Height);
        
        centerOffset.X.Should().BeLessThanOrEqualTo(xError);
        centerOffset.Y.Should().BeLessThanOrEqualTo(yError);
    }
    
    [Test]
    [Repeat(10)]
    public void PutNextRectangle_ShouldReturnRectanglesInCircle()
    {
        var center = random.NextPoint(-100, 100);
        testRectangles = PutRectanglesInCloudLayouter(center);
        
        var actualRectanglesWindow = new RectanglesWindow(testRectangles);
        var radius = (actualRectanglesWindow.Width + actualRectanglesWindow.Height) / 4;
        var actualSquare = (double)testRectangles.Sum(r => r.Width * r.Height);
        var expectedSquare = PolarMath.GetSquareOfCircle(radius);

        var precision = expectedSquare * 0.275;
        actualSquare.Should().BeApproximately(expectedSquare, precision);
    }
    
    [Test]
    [Repeat(10)]
    public void PutNextRectangle_ShouldReturnDontIntersectsRectangles()
    {
        var center = random.NextPoint(-100, 100);
        testRectangles = PutRectanglesInCloudLayouter(center);
        
        IsHaveIntersects(testRectangles).Should().BeFalse();
    }
    
    private Rectangle[] PutRectanglesInCloudLayouter(Point center)
    {
        var rectanglesCount = random.Next(100, 500);
        var circularCloudLayouter = new CircularCloudLayouter(center, 1, 0.5);

        return Enumerable
            .Range(0, rectanglesCount)
            .Select(_ => random.NextSize(10, 50))
            .Select(s => circularCloudLayouter.PutNextRectangle(s))
            .ToArray();
    }

    private static bool IsHaveIntersects(Rectangle[] rectangles)
    {
        for (var i = 0; i < rectangles.Length; i++)
            for (var j = i + 1; j < rectangles.Length; j++) 
                if (rectangles[i].IntersectsWith(rectangles[j]))
                    return true;
        
        return false;
    }

    [Test]
    [Explicit]
    public void PutNextRectangle_FaultedTest()
    {
        testRectangles = PutRectanglesInCloudLayouter(new Point());
        
        testRectangles.Should().BeEmpty();
    }
}