using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;
using Arborescence.Traversal.Adjacency;

namespace AdventOfCode2024;

using V = Vector128<int>;

public static class PartOnePuzzle
{
    public static string Solve(string path, int upperBoundInclusive, int byteCount) =>
        SolveLong(path, upperBoundInclusive, byteCount).ToString(CultureInfo.InvariantCulture);

    public static long SolveLong(string path, int upperBoundInclusive, int byteCount)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines, upperBoundInclusive, byteCount);
    }

    private static long Solve(string[] rows, int upperBoundInclusive, int byteCount)
    {
        var fallingBytes = Helpers.Parse(rows).Take(byteCount).ToFrozenSet();
        Graph graph = new(upperBoundInclusive + 1, fallingBytes);
        var source = V.Zero;
        Dictionary<V, V> predecessorMap = new(upperBoundInclusive) { { source, source } };
        var steps = EnumerableBfs<V>.EnumerateEdges(graph, source);
        var destination = Vector128.Create(upperBoundInclusive, upperBoundInclusive, 0, 0);
        foreach (var step in steps)
        {
            predecessorMap[step.Head] = step.Tail;
            if (step.Head == destination)
                break;
        }

        int count = 0;
        for (var current = destination;; ++count)
        {
            var predecessor = predecessorMap[current];
            if (predecessor == current)
                break;

            current = predecessor;
        }

        return count;
    }
}
