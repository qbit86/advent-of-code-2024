using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<int>;

public static class DistanceCalculator
{
    public static DistanceCalculator<TDistanceMap> Create<TDistanceMap>(TDistanceMap distanceMap)
        where TDistanceMap : IReadOnlyDictionary<V, int>
    {
        if (distanceMap is null)
            throw new ArgumentNullException(nameof(distanceMap));

        return new(distanceMap);
    }
}

public sealed class DistanceCalculator<TDistanceMap>
    where TDistanceMap : IReadOnlyDictionary<V, int>
{
    private readonly TDistanceMap _distanceMap;

    internal DistanceCalculator(TDistanceMap distanceMap) => _distanceMap = distanceMap;

    public int GetSignedDistance(V start, V endInclusive) => _distanceMap[endInclusive] - _distanceMap[start];
}
