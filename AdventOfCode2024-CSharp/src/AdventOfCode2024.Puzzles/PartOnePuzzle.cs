using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Arborescence;
using Arborescence.Models;
using Arborescence.Search.Incidence;

namespace AdventOfCode2024;

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
        bool hasStart = grid.TryFindKey('S', out var startTile);
        if (!hasStart)
            throw new InvalidOperationException($"{nameof(hasStart)} is false.");
        Node source = new(startTile, Helpers.Create(0, 1));
        Dictionary<Node, int> distanceMap = [];
        var edges = AdditiveEnumerableDijkstra<Node, Endpoints<Node>, IncidenceEnumerator<Node, IEnumerator<Node>>, int>
            .EnumerateEdges(graph, source, default(WeightMap), distanceMap);
        foreach (var edge in edges)
        {
            bool hasTile = grid.TryGetValue(edge.Head.Tile, out char c);
            if (!hasTile)
                throw new InvalidOperationException($"{nameof(hasTile)} is false.");
            if (c is not 'E')
                continue;
            int distance = distanceMap[edge.Head];
            return distance;
        }

        throw new UnreachableException();
    }
}
