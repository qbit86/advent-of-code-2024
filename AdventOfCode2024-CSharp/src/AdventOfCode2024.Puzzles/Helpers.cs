using System;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<long>;

internal static class Helpers
{
    private static readonly string[] s_separators = ["\n\n", "\r\n\r\n"];

    internal static Description[] Parse(string path)
    {
        string[] descriptionStrings = SplitDescriptions(File.ReadAllText(path));
        return descriptionStrings.Select(Description.Parse).ToArray();
    }

    private static string[] SplitDescriptions(string text)
    {
        const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
        return text.Split(s_separators, options);
    }

    internal static long ComputeTokens(Description[] descriptions) => descriptions.Sum(ComputeTokens);

    private static long ComputeTokens(Description description)
    {
        var pushes = ComputePushes(description);
        return ComputeTokens(pushes.GetValueOrDefault());
    }

    private static long ComputeTokens(V pushes) => Vector128.Sum(pushes * Vector128.Create(3L, 1L));

    /// <see href="https://en.wikipedia.org/wiki/Cramer%27s_rule" />
    private static V? ComputePushes(Description description)
    {
        long d = Det(description.ADelta, description.BDelta);
        if (d is 0)
            return null;
        long dx = Det(description.Prize, description.BDelta);
        (long x, long rx) = Math.DivRem(dx, d);
        if (rx is not 0)
            return null;
        if (x < 0)
            return null;
        long dy = Det(description.ADelta, description.Prize);
        (long y, long ry) = Math.DivRem(dy, d);
        if (ry is not 0)
            return null;
        if (y < 0)
            return null;
        return Vector128.Create(x, y);
    }

    private static long Det(V left, V right)
    {
        var rightReordered = Vector128.Shuffle(right, Vector128.Create(1, 0));
        var product = left * rightReordered;
        return Vector128.Sum(Vector128.Create(1, -1) * product);
    }
}
