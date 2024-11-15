using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Base;
using TagsCloudVisualization.PointsGenerators;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class ArchimedeanSpiralPointsGeneratorTests
{
    [TestCase(-1, 2, TestName = "RadiusLessThanZero")]
    [TestCase(4, 0, TestName = "AngleOffsetEqualZero")]
    public void ShouldThrowArgumentException(double radius, double angleOffset)
    {
        var pointsGeneratorConstructor = 
            () => new ArchimedeanSpiralPointsGenerator(radius, angleOffset);
        
        pointsGeneratorConstructor.Should().Throw<ArgumentException>();
    }
    
    [TestCase(36, 10)]
    [TestCase(4, 90)]
    [TestCase(8, 45)]
    [TestCase(12, 30)]
    public void GeneratePoints_ShouldReturnPointsInSpiral(double radius, double angleOffset)
    {
        var pointsGenerator = new ArchimedeanSpiralPointsGenerator(radius, angleOffset);
        
        var actualPoints = pointsGenerator
            .GeneratePoints(new Point())
            .Take(1000);

        AssertPointsGeneratingInSpiral(radius, angleOffset, actualPoints);
    }

    private static void AssertPointsGeneratingInSpiral(double radius, double angleOffset, IEnumerable<Point> actualPoints)
    {
        var angle = 0d;
        
        foreach (var actualPoint in actualPoints)
        {
            var expectedRadius = radius * angle / 360;
            var expectedSector = PolarMath.GetSectorOfCircleFromDegrees(angle);
            var (actualRadius, actualAngle) = PolarMath.ConvertToPolarCoordinateSystem(actualPoint);
            var actualSector = PolarMath.GetSectorOfCircleFromRadians(actualAngle);
            
            actualRadius.Should().BeApproximately(expectedRadius, 0.99);
            actualSector.Should().Be(expectedSector);
            
            angle += angleOffset;
        }
    }
}