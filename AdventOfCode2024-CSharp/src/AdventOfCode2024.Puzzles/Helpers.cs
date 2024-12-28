using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using Arborescence;
using Arborescence.Traversal.Adjacency;

namespace AdventOfCode2024;

using V = Vector128<int>;

public static class Helpers
{
    internal static IReadOnlyList<V> Directions { get; } = CreateDirections();

    public static FrozenDictionary<V, int> CreateDistanceMap(Grid grid, V start)
    {
        ArgumentNullException.ThrowIfNull(grid);
        SimpleGraph simpleGraph = new(grid);
        var simpleEdges = EnumerableBfs<V>.EnumerateEdges(simpleGraph, start).ToList();
        return CreateDistanceMap(simpleEdges);
    }

    internal static FrozenDictionary<V, int> CreateDistanceMap(IEnumerable<Endpoints<V>> bfsTree)
    {
        Dictionary<V, int> distanceMap = [];
        PopulateDistanceMap(bfsTree, distanceMap);
        return distanceMap.ToFrozenDictionary();
    }

    private static void PopulateDistanceMap<TDictionary>(IEnumerable<Endpoints<V>> bfsTree, TDictionary distanceMap)
        where TDictionary : IDictionary<V, int>
    {
        using var edgeEnumerator = bfsTree.GetEnumerator();
        if (!edgeEnumerator.MoveNext())
            return;
        var firstEdge = edgeEnumerator.Current;
        distanceMap[firstEdge.Tail] = 0;
        distanceMap[firstEdge.Head] = 1;
        while (edgeEnumerator.MoveNext())
            distanceMap[edgeEnumerator.Current.Head] = distanceMap[edgeEnumerator.Current.Tail] + 1;
    }

    internal static IEnumerable<V> EnumerateVonNeumannNeighborhood(Grid grid, V position, int radius)
    {
        int rowIndexLowerBound = Math.Clamp(position[0] - radius, 0, grid.RowCount - 1);
        int rowIndexUpperBoundInclusive = Math.Clamp(position[0] + radius, 0, grid.RowCount - 1);
        for (int i = rowIndexLowerBound; i <= rowIndexUpperBoundInclusive; ++i)
        {
            int columnCount = grid.GetColumnCount(i);
            int innerRadiusInclusive = radius - Math.Abs(i - position[0]);
            int columnIndexLowerBound = Math.Clamp(position[1] - innerRadiusInclusive, 0, columnCount - 1);
            int columnIndexUpperBoundInclusive = Math.Clamp(position[1] + innerRadiusInclusive, 0, columnCount - 1);
            for (int j = columnIndexLowerBound; j <= columnIndexUpperBoundInclusive; ++j)
            {
                var neighbor = Vector128.Create(i, j, 0, 0);
                if (neighbor != position)
                    yield return neighbor;
            }
        }
    }

    internal static int ManhattanDistance(V start, V endInclusive)
    {
        var difference = start - endInclusive;
        var abs = Vector128.Abs(difference);
        return Vector128.Sum(abs);
    }

    public static int ComputeCheatingSaving(Grid grid, int maxCheatPicoseconds, V start, V cheatingStart)
    {
        ArgumentNullException.ThrowIfNull(grid);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxCheatPicoseconds, 0);
        var distanceMap = CreateDistanceMap(grid, start);
        var distanceCalculator = DistanceCalculator.Create(distanceMap);
        var cheatingEndCandidates = EnumerateVonNeumannNeighborhood(grid, cheatingStart, maxCheatPicoseconds)
            .Where(grid.IsWalkable);
        var cheatingEnd = cheatingEndCandidates.MaxBy(it => distanceCalculator.GetSignedDistance(cheatingStart, it));
        int pathDistance = distanceCalculator.GetSignedDistance(cheatingStart, cheatingEnd);
        int manhattanDistance = ManhattanDistance(cheatingStart, cheatingEnd);
        return pathDistance - manhattanDistance;
    }

    private static IReadOnlyList<V> CreateDirections() =>
    [
        Vector128.Create(0, 1, 0, 0),
        Vector128.Create(1, 0, 0, 0),
        Vector128.Create(0, -1, 0, 0),
        Vector128.Create(-1, 0, 0, 0)
    ];
}
