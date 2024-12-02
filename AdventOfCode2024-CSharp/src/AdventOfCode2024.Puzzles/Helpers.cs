using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdventOfCode2024;

internal static class Helpers
{
    internal static List<int>[] Parse(IReadOnlyList<string> rows) => rows.Select(ParseLine).ToArray();

    private static List<int> ParseLine(string line)
    {
        ReadOnlySpan<char> span = line;
        List<int> result = [];
        var ranges = span.Split(' ');
        foreach (var range in ranges)
            result.Add(int.Parse(span[range], CultureInfo.InvariantCulture));

        return result;
    }
}
