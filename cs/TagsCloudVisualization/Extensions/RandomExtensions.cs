using System.Drawing;

namespace TagsCloudVisualization.Extensions;

public static class RandomExtensions
{
    public static Size NextSize(this Random random, int minLength, int maxLength)
    {
        var width = random.Next(minLength, maxLength);
        var height = random.Next(minLength, maxLength);
        
        return new Size(width, height);
    }
}