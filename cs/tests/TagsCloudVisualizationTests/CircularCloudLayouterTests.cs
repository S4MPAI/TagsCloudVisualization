using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TagsCloudVisualization.Base;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class CircularCloudLayouterTests
{
    private int seed = 232445332;
    private Randomizer random;
    
    [OneTimeSetUp]
    public void Init()
    {
        random = new Randomizer(seed);
    }
    
    [Test]
    public void PutNextRectangle_ShouldReturnRectangleInCenter_FirstRectangle()
    {
        var size = new Size(100, 100);
        var circularCloudLayouter = new CircularCloudLayouter(new Point(0, 0), 1, 0.5);
        
        var actualRectangle = circularCloudLayouter.PutNextRectangle(size);
        var expectedRectangle = new Rectangle(-size.Width / 2, -size.Height / 2, size.Width, size.Height);
        
        actualRectangle.Should().BeEquivalentTo(expectedRectangle);
    }
    
    [Test]
    [Repeat(10)]
    public void PutNextRectangle_ShouldReturnRectanglesInCenter()
    {
        var center = random.NextPoint(-100, 100);
        var rectangles = PutRectanglesInCloudLayouter(center);

        var actualRectanglesWindow = new RectanglesWindow(rectangles);
        var centerOffset = center.Subtract(actualRectanglesWindow.Center).Abs();
        var xError = rectangles.Max(r => r.Width);
        var yError = rectangles.Max(r => r.Height);
        
        centerOffset.X.Should().BeLessThanOrEqualTo(xError);
        centerOffset.Y.Should().BeLessThanOrEqualTo(yError);
    }
    
    [Test]
    [Repeat(10)]
    public void PutNextRectangle_ShouldReturnRectanglesInCircle()
    {
        var center = random.NextPoint(-100, 100);
        var rectangles = PutRectanglesInCloudLayouter(center);
        
        var actualRectanglesWindow = new RectanglesWindow(rectangles);
        var radius = (actualRectanglesWindow.Width + actualRectanglesWindow.Height) / 4;
        var actualSquare = (double)rectangles.Sum(r => r.Width * r.Height);
        var expectedSquare = PolarMath.GetSquareOfCircle(radius);

        var precision = expectedSquare * 0.275;
        actualSquare.Should().BeApproximately(expectedSquare, precision);
    }
    
    [Test]
    [Repeat(10)]
    public void PutNextRectangle_ShouldReturnDontIntersectsRectangles()
    {
        var center = random.NextPoint(-100, 100);
        var rectangles = PutRectanglesInCloudLayouter(center);
        
        IsHaveIntersects(rectangles).Should().BeFalse();
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
}