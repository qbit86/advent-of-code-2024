using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AdventOfCode2024;

public static class PartOnePuzzle
{
    public static string Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        long result = SolveLong(path);
        return result.ToString(CultureInfo.InvariantCulture);
    }

    public static long SolveLong(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return SolveLong(lines);
    }

    private static long SolveLong(string[] rows)
    {
        ArgumentNullException.ThrowIfNull(rows);
        var originalEdges = rows.Select(Helpers.ParseEdge).ToArray();
        var reverseEdges = originalEdges.Select(Helpers.ReverseEdge).ToArray();
        var allEdges = originalEdges.Concat(reverseEdges).ToArray();
        var lookup = allEdges.ToLookup(edge => edge.Tail, edge => edge.Head);
        var neighborMap = lookup
            .ToFrozenDictionary(it => it.Key, it => it.ToFrozenSet());
        List<string[]> triples = [];
        var computersOfInterest = neighborMap.Keys.Where(it => it.StartsWith('t')).ToList();
        foreach (string computer in computersOfInterest)
        {
            var neighbors = neighborMap[computer];
            var neighborList = neighbors.ToList();
            for (int i = 0; i < neighborList.Count; ++i)
            {
                string left = neighborList[i];
                for (int j = i + 1; j < neighborList.Count; ++j)
                {
                    string right = neighborList[j];
                    if (neighborMap[left].Contains(right))
                    {
                        string[] triple = [computer, left, right];
                        Array.Sort(triple);
                        triples.Add(triple);
                    }
                }
            }
        }

        var distinctTriples = triples.Distinct(ArrayComparer.Instance).ToList();
        return distinctTriples.Count;
    }
}

file sealed class ArrayComparer : IComparer<string[]>, IEqualityComparer<string[]>
{
    internal static ArrayComparer Instance { get; } = new();

    public int Compare(string[]? x, string[]? y) => x.AsSpan().SequenceCompareTo(y.AsSpan());

    public bool Equals(string[]? x, string[]? y) => x.AsSpan().SequenceEqual(y.AsSpan());

    public int GetHashCode(string[] obj) =>
        obj.Aggregate(0, (current, it) => current ^ it.GetHashCode(StringComparison.Ordinal));
}
