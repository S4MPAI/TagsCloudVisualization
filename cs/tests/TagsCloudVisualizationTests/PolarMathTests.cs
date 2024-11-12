using System.Drawing;
using NUnit.Framework;
using TagsCloudVisualization.Base;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class PolarMathTests
{
    [TestCase(0, ExpectedResult = 0, TestName = "DegreesEqualZero")]
    [TestCase(30, ExpectedResult = Math.PI / 6, TestName = "DegreesEqual30")]
    [TestCase(90, ExpectedResult = Math.PI / 2, TestName = "DegreesEqual90")]
    [TestCase(120, ExpectedResult = 2 * Math.PI / 3, TestName = "DegreesEqual120")]
    [TestCase(180, ExpectedResult = Math.PI, TestName = "DegreesEqual180")]
    [TestCase(270, ExpectedResult =  3 * Math.PI / 2, TestName = "DegreesEqual270")]
    [TestCase(315, ExpectedResult =  7 * Math.PI / 4, TestName = "DegreesEqual315")]
    [TestCase(360, ExpectedResult = 2 * Math.PI, TestName = "DegreesEqual360")]
    public double ConvertToRadians_ShouldReturnExpectedResult(double degrees) => 
        PolarMath.ConvertToRadians(degrees);

    [TestCase( 0.5 * Math.PI, ExpectedResult = 0.25, TestName = "RadiusEqualHalfOfPi")]
    [TestCase(Math.PI, ExpectedResult = 0.5, TestName = "RadiusEqualPi")]
    [TestCase(1.5 * Math.PI, ExpectedResult = 0.75, TestName = "RadiusEqualOneAndHalfPi")]
    [TestCase(2 * Math.PI, ExpectedResult = 1, TestName = "RadiusEqualTwoPi")]
    public double GetOffsetPerRadianForArchimedeanSpiral_ShouldReturnExpectedResult(double radius) => 
        PolarMath.GetOffsetPerRadianForArchimedeanSpiral(radius);
    
    [TestCaseSource(nameof(_getOffsetPerRadianForArchimedeanSpiralTestCases))]
    public Point ConvertToCartesianCoordinateSystem_ShouldReturnExpectedResult(double polarAngle) => 
        PolarMath.ConvertToCartesianCoordinateSystem(10, polarAngle);

    private static object[] _getOffsetPerRadianForArchimedeanSpiralTestCases =
    {
        new TestCaseData(Math.PI / 2).Returns(new Point(0, 10)).SetName("PolarAngleEqualHalfOfPi"),
        new TestCaseData(Math.PI).Returns(new Point(-10, 0)).SetName("PolarAngleEqualPi"),
        new TestCaseData(0.25 * Math.PI).Returns(new Point(7, 7)).SetName("PolarAngleEqualQuarterOfPi"),
        new TestCaseData(1.5 * Math.PI).Returns(new Point(0, -10)).SetName("PolarAngleEqualHalfOfPi"),
    };
}