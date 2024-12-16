using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Arborescence;
using Arborescence.Models;
using Arborescence.Search.Incidence;

namespace AdventOfCode2024;

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
        bool hasStartTile = grid.TryFindKey('S', out var startTile);
        if (!hasStartTile)
            throw new InvalidOperationException($"{nameof(hasStartTile)} is false.");
        bool hasEndTile = grid.TryFindKey('E', out var endTile);
        if (!hasEndTile)
            throw new InvalidOperationException($"{nameof(hasEndTile)} is false.");

        Dictionary<Node, int> distanceMap = [];
        Node startNode = new(startTile, Helpers.Create(0, 1));
        int score = GetScoreOrMax(new Graph(grid), startNode, it => it.Tile == endTile, distanceMap);
        if (score is int.MaxValue)
            throw new InvalidOperationException("MaxValue");

        var endNodePairs = distanceMap.Where(it => it.Key.Tile == endTile).ToList();
        var endNode = endNodePairs.Single(it => it.Value == score).Key;

        Dictionary<Node, int> reversedDistanceMap = [];
        int reversedScore = GetScoreOrMax(new ReversedGraph(grid), endNode, it => it == startNode, reversedDistanceMap);
        Debug.Assert(score == reversedScore);

        var bestNodeCandidates = distanceMap.Keys.Concat(reversedDistanceMap.Keys).Distinct();
        var bestNodes = bestNodeCandidates
            .Where(it =>
                distanceMap.GetValueOrDefault(it, short.MaxValue) +
                reversedDistanceMap.GetValueOrDefault(it, short.MaxValue) == score);
        var bestTiles = bestNodes.Select(it => it.Tile).Distinct();
        return bestTiles.Count();
    }

    private static int GetScoreOrMax<TGraph>(
        TGraph graph, Node source, Predicate<Node> shouldStop, Dictionary<Node, int> distanceMap)
        where TGraph : IHeadIncidence<Node, Endpoints<Node>>,
        IOutEdgesIncidence<Node, IncidenceEnumerator<Node, IEnumerator<Node>>>
    {
        var edges =
            AdditiveEnumerableDijkstra<Node, Endpoints<Node>, IncidenceEnumerator<Node, IEnumerator<Node>>, int>
                .EnumerateEdges(graph, source, default(WeightMap), distanceMap);
        int score = int.MaxValue;
        foreach (var edge in edges)
        {
            if (score < int.MaxValue)
            {
                if (distanceMap.GetValueOrDefault(edge.Tail, int.MaxValue) >= score)
                    break;
            }
            else if (shouldStop(edge.Head))
            {
                score = distanceMap.GetValueOrDefault(edge.Head, int.MaxValue);
            }
        }

        return score;
    }
}
