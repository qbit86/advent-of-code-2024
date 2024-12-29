using System;
using System.Globalization;

namespace AdventOfCode2024;

internal static class Helpers
{
    internal static long GetNumericPart(string code)
    {
        var trimmed = code.AsSpan().TrimStart('0').TrimEnd('A');
        return long.Parse(trimmed, CultureInfo.InvariantCulture);
    }
}
