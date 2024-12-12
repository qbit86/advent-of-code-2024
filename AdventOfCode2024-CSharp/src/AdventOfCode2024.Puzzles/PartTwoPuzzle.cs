using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;
using Arborescence;
using Arborescence.Traversal.Adjacency;

namespace AdventOfCode2024;

using Node = Endpoints<Vector128<int>>;
using V = Vector128<int>;

public static class PartTwoPuzzle
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
        HashSet<Node> nodes = [];
        foreach (var plot in grouping)
        {
            var outEdges = perimeterGraph.EnumerateOutEdges(plot);
            while (outEdges.MoveNext())
            {
                var edge = outEdges.Current;
                if (grid.GetValueOrDefault(edge.Tail) != grid.GetValueOrDefault(edge.Head))
                {
                    bool added = nodes.Add(edge);
                    Debug.Assert(added);
                }
            }
        }

        int perimeter = ComputePerimeter(nodes);
        return int.BigMul(area, perimeter);
    }

    private static int ComputePerimeter(IReadOnlySet<Node> nodes)
    {
        MetaGraph metaGraph = new(nodes);
        Dictionary<Node, Node> representativeByNode = [];
        foreach (var source in nodes)
        {
            if (representativeByNode.ContainsKey(source))
                continue;
            var steps = EnumerableDfs<Node>.EnumerateVertices(metaGraph, source);
            foreach (var step in steps)
                representativeByNode[step] = source;
        }

        return representativeByNode.Values.Distinct().Count();
    }
}
