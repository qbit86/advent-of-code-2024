using System;
using System.Collections.Generic;
using System.Globalization;

namespace AdventOfCode2024;

internal static class Helpers
{
    private const StringSplitOptions SplitOptions =
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    internal static (int[] LeftNumbers, int[] RightNumbers) Parse<TRows>(TRows rows)
        where TRows : IReadOnlyList<string>
    {
        int[] leftNumbers = GC.AllocateUninitializedArray<int>(rows.Count);
        int[] rightNumbers = GC.AllocateUninitializedArray<int>(rows.Count);
        Span<Range> ranges = stackalloc Range[3];
        for (int i = 0; i < rows.Count; i++)
        {
            string line = rows[i];
            _ = line.AsSpan().Split(ranges, ' ', SplitOptions);
            int leftNumber = int.Parse(line.AsSpan()[ranges[0]], CultureInfo.InvariantCulture);
            leftNumbers[i] = leftNumber;
            int rightNumber = int.Parse(line.AsSpan()[ranges[1]], CultureInfo.InvariantCulture);
            rightNumbers[i] = rightNumber;
        }

        return (leftNumbers, rightNumbers);
    }
}
