using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal static class Helpers
{
    internal static V[] Directions { get; } =
    [
        Vector128.Create(1, 0, 0, 0),
        Vector128.Create(0, 1, 0, 0),
        Vector128.Create(-1, 0, 0, 0),
        Vector128.Create(0, -1, 0, 0)
    ];

    internal static V[] Parse(string[] lines) => lines.Select(ParseSingle).ToArray();

    private static V ParseSingle(string line)
    {
        var span = line.AsSpan();
        Span<Range> ranges = stackalloc Range[3];
        int count = span.Split(ranges, ',', StringSplitOptions.TrimEntries);
        ArgumentOutOfRangeException.ThrowIfNotEqual(count, 2);
        int x = int.Parse(span[ranges[0]], CultureInfo.InvariantCulture);
        int y = int.Parse(span[ranges[1]], CultureInfo.InvariantCulture);
        return Vector128.Create(x, y, 0, 0);
    }
}
