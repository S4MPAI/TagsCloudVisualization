using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
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
    public void PutNextRectangle_ShouldReturnRectangleInCenter_IsFirstRectangle()
    {
        var size = new Size(100, 100);
        var circularCloudLayouter = new CircularCloudLayouter(new Point(0, 0), 1, 0.5);
        
        var actualRectangle = circularCloudLayouter.PutNextRectangle(size);
        var expectedRectangle = new Rectangle(-size.Width / 2, -size.Height / 2, size.Width, size.Height);
        
        actualRectangle.Should().BeEquivalentTo(expectedRectangle);
    }

    [Test]
    [Repeat(10)]
    public void PutNextRectangle_ShouldReturnDontIntersectsRectangles()
    {
        var rectanglesCount = random.Next(100, 500);
        var circularCloudLayouter = new CircularCloudLayouter(new Point(0, 0), 1, 0.5);
        var rectangles = Enumerable
            .Range(0, rectanglesCount)
            .Select(_ => random.NextSize(10, 50))
            .Select(s => circularCloudLayouter.PutNextRectangle(s))
            .ToArray();
        
        IsHaveIntersects(rectangles).Should().BeFalse();
    }

    private bool IsHaveIntersects(Rectangle[] rectangles)
    {
        for (int i = 0; i < rectangles.Length; i++)
        {
            for (int j = 0; j < rectangles.Length; j++)
            {
                if (i == j)
                    continue;
                
                if (rectangles[i].IntersectsWith(rectangles[j]))
                    return true;
            }
        }
        
        return false;
    }
}