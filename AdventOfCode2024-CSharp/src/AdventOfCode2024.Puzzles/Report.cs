using System;
using System.Collections.Generic;

namespace AdventOfCode2024;

internal static class Report
{
    internal static Report<TLevels> Create<TLevels>(TLevels levels, int excludedIndex)
        where TLevels : IReadOnlyList<int> =>
        new(levels, excludedIndex);
}

internal readonly record struct Report<TLevels>(TLevels Levels, int ExcludedIndex) where TLevels : IReadOnlyList<int>
{
    private bool TryDecrement(int index, out int decrementedIndex)
    {
        int candidate = index - 1;
        decrementedIndex = candidate == ExcludedIndex ? candidate - 1 : candidate;
        return decrementedIndex >= 0;
    }

    private bool TryIncrement(int index, out int incrementedIndex)
    {
        int candidate = index + 1;
        incrementedIndex = candidate == ExcludedIndex ? candidate + 1 : candidate;
        return incrementedIndex < Levels.Count;
    }

    internal bool IsLevelSafeLeft(int index, out int sign)
    {
        if (!TryDecrement(index, out int previousIndex))
        {
            sign = default;
            return true;
        }

        int signedDifference = Levels[index] - Levels[previousIndex];
        sign = Math.Sign(signedDifference);
        return Math.Abs(signedDifference) is >= 1 and <= 3;
    }

    internal bool IsLevelSafeRight(int index, out int sign)
    {
        if (!TryIncrement(index, out int nextIndex))
        {
            sign = default;
            return true;
        }

        int signedDifference = Levels[nextIndex] - Levels[index];
        sign = Math.Sign(signedDifference);
        return Math.Abs(signedDifference) is >= 1 and <= 3;
    }
}
