using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Arborescence;
using Arborescence.Models;
using Arborescence.Traversal;
using Arborescence.Traversal.Adjacency;

namespace AdventOfCode2024;

public static class PartTwoPuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(IReadOnlyList<string> rows)
    {
        var separator = rows.Index().First(it => it.Item.Length is 0);
        string[] rules = rows.Take(separator.Index).ToArray();
        string[] updates = rows.Skip(separator.Index + 1).ToArray();
        return Solve(rules, updates);
    }

    private static long Solve(string[] ruleLines, string[] updateLines)
    {
        var edges = ruleLines.Select(Helpers.ParseEndpoints).ToArray();
        var updates = updateLines.Select(Helpers.ParseUpdate).ToArray();
        return Solve(edges, updates);
    }

    private static long Solve(IReadOnlyList<Endpoints<int>> edges, IReadOnlyList<List<int>> updates)
    {
        var outNeighborsMap = edges.ToLookup(it => it.Tail, it => it.Head)
            .ToFrozenDictionary(it => it.Key, it => it.ToFrozenSet());
        NonTransitiveComparer comparer = new(outNeighborsMap);
        var incorrectlyOrderedUpdates = updates.Where(IsIncorrectlyOrdered);
        var correctlyOrderedUpdates = incorrectlyOrderedUpdates.Select(Sort);
        var middlePages = correctlyOrderedUpdates.Select(it => it[it.Count >> 1]);
        return middlePages.Sum();

        bool IsIncorrectlyOrdered(IReadOnlyList<int> pages) =>
            pages.Zip(pages.Skip(1)).Any(pair => comparer.Compare(pair.First, pair.Second) > 0);

        List<int> Sort(IReadOnlyList<int> pages)
        {
            List<int> sortedPages = [];
            var inducedOutNeighborsMap = pages.ToFrozenDictionary(
                it => it, it => outNeighborsMap.GetValueOrDefault(it, []).Intersect(pages).ToFrozenSet());
            var graph = ReadOnlyAdjacencyGraph<int>.FromFrozenSets(inducedOutNeighborsMap);
            var handler = CreateDfsHandler(graph);
            handler.FinishVertex += (_, v) => sortedPages.Add(v);
            EagerDfs<int, FrozenSet<int>.Enumerator>.Traverse(graph, pages, handler);
            sortedPages.Reverse();
            return sortedPages;
        }
    }

    private static DfsHandler<int, Endpoints<int>, TGraph> CreateDfsHandler<TGraph>(TGraph _) => new();
}
