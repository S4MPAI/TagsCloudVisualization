using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
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
    
    [TestCaseSource(nameof(GeneratePointsTestCases))]
    public Point GeneratePoints_ShouldReturnExpectedPoint(double radius, double angleOffset, int pointIndex)
    {
        var pointsGenerator = new ArchimedeanSpiralPointsGenerator(radius, angleOffset);
        
        var actualPoint = pointsGenerator
            .GeneratePoints(new Point())
            .Skip(pointIndex)
            .First();
        
        return actualPoint;
    }

    public static object[] GeneratePointsTestCases =
    {
        new TestCaseData(3, 360, 0).Returns(new Point(0, 0)).SetName("GetFirstPoint"),
        new TestCaseData(1, 360, 1).Returns(new Point(1, 0)).SetName("GetSecondPointAndAngleOffsetEqual360"),
        new TestCaseData(2, 180, 1).Returns(new Point(-1, 0)).SetName("GetSecondPointAndAngleOffsetEqual180"),
        new TestCaseData(4, 90, 1).Returns(new Point(0, 1)).SetName("GetSecondPointAndAngleOffsetEqual90"),
        new TestCaseData(4, 270, 1).Returns(new Point(0, -3)).SetName("GetSecondPointAndAngleOffsetEqual270"),
        new TestCaseData(8, 45, 1).Returns(new Point(1, 1)).SetName("GetSecondPointAndAngleOffsetEqual45"),
        new TestCaseData(3, 360, 2).Returns(new Point(6, 0)).SetName("GetThirdPointAndAngleOffsetEqual360")
    };
}