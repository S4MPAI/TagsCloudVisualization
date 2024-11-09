using System.Drawing;

namespace TagsCloudVisualization;

public interface ICloudLayouter
{
    Rectangle PutNextRectangle(Rectangle rectangle);
}