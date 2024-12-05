using System;
using System.Collections.Generic;
using System.Globalization;
using Arborescence;

namespace AdventOfCode2024;

internal static class Helpers
{
    private const StringSplitOptions Options = StringSplitOptions.TrimEntries;

    internal static Endpoints<int> ParseEndpoints(string line)
    {
        Span<Range> ranges = stackalloc Range[3];
        int count = line.AsSpan().Split(ranges, '|', Options);
        ArgumentOutOfRangeException.ThrowIfNotEqual(count, 2, nameof(line));
        int tail = int.Parse(line[ranges[0]], CultureInfo.InvariantCulture);
        int head = int.Parse(line[ranges[1]], CultureInfo.InvariantCulture);
        return new(tail, head);
    }

    internal static List<int> ParseUpdate(string line)
    {
        List<int> pages = [];
        var ranges = line.AsSpan().Split(',');
        foreach (var range in ranges)
        {
            int page = int.Parse(line[range], CultureInfo.InvariantCulture);
            pages.Add(page);
        }

        return pages;
    }
}
