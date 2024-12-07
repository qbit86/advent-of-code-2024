using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AdventOfCode2024;

internal readonly record struct Equation(long TestValue, int[] Numbers)
{
    private bool PrintMembers(StringBuilder builder)
    {
        _ = builder.Append($"{nameof(TestValue)} = ").Append(TestValue);
        _ = builder.Append($", {nameof(Numbers)} = [").Append(string.Join(", ", Numbers)).Append(']');
        return true;
    }

    internal static Equation Parse(string line)
    {
        var span = line.AsSpan();
        Span<Range> ranges = stackalloc Range[3];
        int count = span.Split(ranges, ':', StringSplitOptions.TrimEntries);
        ArgumentOutOfRangeException.ThrowIfNotEqual(count, 2, nameof(line));
        long testValue = long.Parse(span[ranges[0]], CultureInfo.InvariantCulture);
        var numbers = ParseNumbers(span[ranges[1]]);
        return new(testValue, [.. numbers]);
    }

    private static List<int> ParseNumbers(ReadOnlySpan<char> span)
    {
        List<int> numbers = [];
        var ranges = span.Split(' ');
        foreach (var range in ranges)
        {
            int number = int.Parse(span[range], CultureInfo.InvariantCulture);
            numbers.Add(number);
        }

        return numbers;
    }
}
