using System;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;
using Arborescence.Traversal.Adjacency;

namespace AdventOfCode2024;

using P = Vector128<int>;

public static class PartOnePuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(string[] rows)
    {
        Grid grid = new(rows);
        Graph graph = new(grid);
        var trailheads = grid.Where(it => it.Value is 0).Select(it => it.Key);
        return trailheads.Sum(ComputeScore);

        long ComputeScore(P trailhead)
        {
            var steps = EnumerableDfs<P>.EnumerateVertices(graph, trailhead);
            return steps.Select(it => grid.GetHeightOrDefault(it)).Count(it => it is 9);
        }
    }
}
