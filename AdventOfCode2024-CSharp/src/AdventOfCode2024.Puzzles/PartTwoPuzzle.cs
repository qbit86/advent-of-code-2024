using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;
using Arborescence.Traversal.Adjacency;

namespace AdventOfCode2024;

using P = Vector128<int>;

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
        Graph graph = new(grid);
        var trailheads = grid.Where(it => it.Value is 0).Select(it => it.Key);
        return trailheads.Sum(ComputeRating);

        long ComputeRating(P trailhead)
        {
            var steps = EnumerableDfs<P>.EnumerateVertices(graph, trailhead);
            var finalPoints = steps.Where(it => grid.GetHeightOrDefault(it) is 9);
            return finalPoints.Sum(it => ComputeTrailRating([], graph, trailhead, it));
        }
    }

    private static long GetFromCacheOrComputeTrailRating(
        Dictionary<P, long> cache, Graph graph, P source, P destination)
    {
        if (cache.TryGetValue(destination, out long cachedRating))
            return cachedRating;
        long rating = ComputeTrailRating(cache, graph, source, destination);
        cache.Add(destination, rating);
        return rating;
    }

    private static long ComputeTrailRating(
        Dictionary<P, long> cache, Graph graph, P source, P destination)
    {
        Debug.Assert(!cache.ContainsKey(destination));
        if (source == destination)
            return 1;
        long rating = 0;
        var neighbors = graph.EnumerateInNeighbors(destination);
        while (neighbors.MoveNext())
            rating += GetFromCacheOrComputeTrailRating(cache, graph, source, neighbors.Current);
        return rating;
    }
}
