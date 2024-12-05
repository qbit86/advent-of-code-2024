using System;
using System.Collections.Frozen;
using System.Collections.Generic;

namespace AdventOfCode2024;

internal sealed class NonTransitiveComparer : IComparer<int>
{
    private readonly IReadOnlyDictionary<int, FrozenSet<int>> _outNeighborsMap;

    public NonTransitiveComparer(IReadOnlyDictionary<int, FrozenSet<int>> outNeighborsMap) =>
        _outNeighborsMap = outNeighborsMap;

    public int Compare(int x, int y)
    {
        if (x == y)
            return 0;
        if (_outNeighborsMap.TryGetValue(x, out var xNeighbors) && xNeighbors.Contains(y))
            return -1;
        return 1;
    }
}
