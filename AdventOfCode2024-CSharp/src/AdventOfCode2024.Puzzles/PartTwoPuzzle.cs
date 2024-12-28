using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;
using Arborescence.Traversal.Adjacency;

namespace AdventOfCode2024;

using V = Vector128<int>;

public static class PartTwoPuzzle
{
    public static long Solve(string path, int cheatPicosecondsUpperBoundInclusive, int savedPicosecondsLowerBound)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines, cheatPicosecondsUpperBoundInclusive, savedPicosecondsLowerBound);
    }

    internal static long Solve(string[] rows, int cheatPicosecondsUpperBoundInclusive, int savedPicosecondsLowerBound)
    {
        Grid grid = new(rows);
        bool hasStart = grid.TryFindKey('S', out var start);
        if (!hasStart)
            throw new ArgumentException(nameof(hasStart), nameof(rows));

        SimpleGraph simpleGraph = new(grid);
        var simpleEdges = EnumerableBfs<V>.EnumerateEdges(simpleGraph, start).ToList();
        var distanceMap = Helpers.CreateDistanceMap(simpleEdges);
        var distanceCalculator = DistanceCalculator.Create(distanceMap);

        int cheatCount = 0;
        var cheatingStarts = simpleEdges.Select(it => it.Tail);
        foreach (var cheatingStart in cheatingStarts)
        {
            var neighbors =
                Helpers.EnumerateVonNeumannNeighborhood(grid, cheatingStart, cheatPicosecondsUpperBoundInclusive);
            foreach (var cheatingEndInclusive in neighbors)
            {
                if (!grid.IsWalkable(cheatingEndInclusive))
                    continue;
                int pathDistance = distanceCalculator.GetSignedDistance(cheatingStart, cheatingEndInclusive);
                if (pathDistance < 0)
                    continue;
                Debug.Assert(pathDistance > 0);
                int manhattanDistance = Helpers.ManhattanDistance(cheatingStart, cheatingEndInclusive);
                int savedPicoseconds = pathDistance - manhattanDistance;
                if (savedPicoseconds >= savedPicosecondsLowerBound)
                    ++cheatCount;
            }
        }

        return cheatCount;
    }
}
