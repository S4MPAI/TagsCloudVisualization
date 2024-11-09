using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class ArchimedeanSpiralPointsGeneratorTests
{
    [TestCase(-1, 2)]
    [TestCase(4, -2)]
    public void ShouldThrowArgumentException(double radius, double angleOffset)
    {
        var pointsGeneratorConstructor = () => new ArchimedeanSpiralPointsGenerator(radius, angleOffset);
        
        pointsGeneratorConstructor.Should().Throw<ArgumentException>();
    }
    
    [TestCaseSource(nameof(GeneratePointsTestCases))]
    public Point GeneratePoints_ShouldReturnExpectedPoint(double radius, double angleOffset, int pointIndex)
    {
        var pointsGenerator = new ArchimedeanSpiralPointsGenerator(radius, angleOffset);
        
        var actualPoints = pointsGenerator.GeneratePoints(new Point()).Skip(pointIndex).First();
        
        return actualPoints;
    }

    public static object[] GeneratePointsTestCases =
    {
        new TestCaseData(3, 360, 0).Returns(new Point(0, 0)).SetName("IfGetFirstPoint"),
        new TestCaseData(1, 360, 1).Returns(new Point(1, 0)).SetName("IfGetSecondPoint_AndAngleOffsetEqual360"),
        new TestCaseData(2, 180, 1).Returns(new Point(-1, 0)).SetName("IfGetSecondPoint_AndAngleOffsetEqual180"),
        new TestCaseData(4, 90, 1).Returns(new Point(0, 1)).SetName("IfGetSecondPoint_AndAngleOffsetEqual90"),
        new TestCaseData(4, 270, 1).Returns(new Point(0, -3)).SetName("IfGetSecondPoint_AndAngleOffsetEqual270"),
        new TestCaseData(8, 45, 1).Returns(new Point(1, 1)).SetName("IfGetSecondPoint_AndAngleOffsetEqual45"),
        new TestCaseData(3, 360, 2).Returns(new Point(6, 0)).SetName("IfGetThirdPoint_AndAngleOffsetEqual360")
    };
}