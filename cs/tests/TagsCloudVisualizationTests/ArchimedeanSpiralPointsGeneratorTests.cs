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
        var pointsGeneratorConstructor = () => new ArchimedeanSpiralPointsGenerator(radius, angleOffset);
        
        pointsGeneratorConstructor.Should().Throw<ArgumentException>();
    }
    
    [TestCase(1, 10)]
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
        var angleRadiansOffset = PolarMath.ConvertToRadians(angleOffset);
        var (_, lastAngle) = PolarMath.ConvertToPolarCoordinateSystem(new Point());
        
        foreach (var actualPoint in actualPoints)
        {
            var expectedRadius = radius * angle / 360;
            var (actualRadius, actualAngle) = PolarMath.ConvertToPolarCoordinateSystem(actualPoint);
            
            actualRadius.Should().BeApproximately(expectedRadius, 0.99);
            actualAngle.Should().BeGreaterThanOrEqualTo(lastAngle);
            
            lastAngle = actualAngle + angleRadiansOffset >= 2 * Math.PI ? 0 : lastAngle;
            angle += angleOffset;
        }
    }
}