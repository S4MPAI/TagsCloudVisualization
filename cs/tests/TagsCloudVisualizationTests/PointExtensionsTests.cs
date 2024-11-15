using System.Drawing;
using NUnit.Framework;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualizationTests;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class PointExtensionsTests
{
    [TestCaseSource(nameof(_subtractTestCases))]
    public Point Subtract_ShouldReturnExpectedPoint(Point point1, Point point2) =>
        point1.Subtract(point2);

    private static TestCaseData[] _subtractTestCases =
    {
        new TestCaseData(new Point(5, 3), new Point(2, 3)).Returns(new Point(3, 0)).SetName("ExpectedPointPositive"),
        new TestCaseData(new Point(5, 3), new Point(9, 7)).Returns(new Point(-4, -4)).SetName("ExpectedPointNegative")
    };

    [TestCaseSource(nameof(_absTestCases))]
    public Point Abs_ShouldReturnExpectedPoint(Point point) =>
        point.Abs();

    private static TestCaseData[] _absTestCases =
    {
        new TestCaseData(new Point(5, 3)).Returns(new Point(5, 3)).SetName("PointPositive"),
        new TestCaseData(new Point(-6, -7)).Returns(new Point(6, 7)).SetName("PointNegative")
    };
}