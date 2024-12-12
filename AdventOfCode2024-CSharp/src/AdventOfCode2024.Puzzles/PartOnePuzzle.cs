using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;
using Arborescence.Traversal.Adjacency;

namespace AdventOfCode2024;

using V = Vector128<int>;

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
        RegionGraph regionGraph = new(grid);
        Dictionary<V, V> representativeByPlot = [];
        var plots = grid.Select(it => it.Key);
        foreach (var source in plots)
        {
            if (representativeByPlot.ContainsKey(source))
                continue;
            var steps = EnumerableDfs<V>.EnumerateVertices(regionGraph, source);
            foreach (var step in steps)
                representativeByPlot[step] = source;
        }

        var lookup = representativeByPlot.ToLookup(it => it.Value, it => it.Key);
        return lookup.Sum(grouping => ComputePrice(grid, grouping));
    }

    private static long ComputePrice(Grid grid, IGrouping<V, V> grouping)
    {
        int area = grouping.Count();
        var representative = grouping.Key;
        char kind = grid[representative];
        PerimeterGraph perimeterGraph = new(grid, kind);
        int perimeter = 0;
        foreach (var plot in grouping)
        {
            var neighbors = perimeterGraph.EnumerateOutNeighbors(plot);
            while (neighbors.MoveNext())
            {
                var neighbor = neighbors.Current;
                perimeter += Convert.ToInt32(grid.GetValueOrDefault(plot) != grid.GetValueOrDefault(neighbor));
            }
        }

        return int.BigMul(area, perimeter);
    }
}
