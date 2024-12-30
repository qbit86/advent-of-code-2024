using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace AdventOfCode2024;

public static class PartTwoPuzzle
{
    public static string Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] array = SolveArray(path);
        return string.Join(",", array);
    }

    private static string[] SolveArray(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return SolveArray(lines);
    }

    private static string[] SolveArray(string[] rows)
    {
        ArgumentNullException.ThrowIfNull(rows);
        var originalEdges = rows.Select(Helpers.ParseEdge).ToArray();
        var reverseEdges = originalEdges.Select(Helpers.ReverseEdge).ToArray();
        var allEdges = originalEdges.Concat(reverseEdges).ToArray();
        var lookup = allEdges.ToLookup(edge => edge.Tail, edge => edge.Head);
        var neighborMap = lookup
            .ToFrozenDictionary(it => it.Key, it => it.ToFrozenSet());
        var maxCliqueSet = FindMaxClique(neighborMap);
        string[] maxCliqueArray = [.. maxCliqueSet];
        Array.Sort(maxCliqueArray);
        return maxCliqueArray;
    }

    private static IReadOnlySet<string> FindMaxClique<TNeighborMap>(TNeighborMap neighborMap)
        where TNeighborMap : IReadOnlyDictionary<string, FrozenSet<string>>
    {
        HashSet<string> allVertices = [..neighborMap.Keys];
        return BronKerbosch(ImmutableHashSet<string>.Empty, allVertices, []);

        IReadOnlySet<string> BronKerbosch(ImmutableHashSet<string> r, HashSet<string> p, HashSet<string> x)
        {
            if (p.Count is 0 && x.Count is 0)
                return r;

            string pivot = p.Union(x).First();
            var pivotNeighbors = neighborMap.GetValueOrDefault(pivot, FrozenSet<string>.Empty);

            IReadOnlySet<string> maxClique = FrozenSet<string>.Empty;
            foreach (string v in p.Except(pivotNeighbors))
            {
                var neighborsOfV = neighborMap.GetValueOrDefault(v, FrozenSet<string>.Empty);

                var newR = r.Add(v);
                HashSet<string> newP = [..p.Intersect(neighborsOfV)];
                HashSet<string> newX = [..x.Intersect(neighborsOfV)];

                var currentClique = BronKerbosch(newR, newP, newX);
                if (currentClique.Count > maxClique.Count)
                    maxClique = currentClique;

                _ = p.Remove(v);
                _ = x.Add(v);
            }

            return maxClique;
        }
    }
}
