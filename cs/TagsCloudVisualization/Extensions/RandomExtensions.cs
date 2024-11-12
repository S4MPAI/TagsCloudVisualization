using System.Drawing;

namespace TagsCloudVisualization.Extensions;

public static class RandomExtensions
{
    public static Size NextSize(this Random random, int minLength, int maxLength)
    {
        if (minLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(minLength), minLength, "minLength must be greater than zero.");
        
        var width = random.Next(minLength, maxLength);
        var height = random.Next(minLength, maxLength);
        
        return new Size(width, height);
    }
}