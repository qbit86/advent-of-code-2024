using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;
using Arborescence.Traversal.Adjacency;

namespace AdventOfCode2024;

using V = Vector128<int>;

public static class PartTwoPuzzle
{
    public static string Solve(string path, int upperBoundInclusive, int byteCountLowerBound)
    {
        var v = SolveVector(path, upperBoundInclusive, byteCountLowerBound);
        return $"{v[0]},{v[1]}";
    }

    public static V SolveVector(string path, int upperBoundInclusive, int byteCountLowerBound)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines, upperBoundInclusive, byteCountLowerBound);
    }

    private static V Solve(string[] rows, int upperBoundInclusive, int byteCountLowerBound)
    {
        var allFallingBytes = Helpers.Parse(rows).ToArray();
        // Binary search.
        int low = byteCountLowerBound;
        int highInclusive = allFallingBytes.Length - 1;
        while (low <= highInclusive)
        {
            int i = (int)(((uint)highInclusive + (uint)low) >> 1);
            bool currentContainsPath = ContainsPath(upperBoundInclusive, allFallingBytes.Take(i).ToFrozenSet());
            bool nextContainsPath = ContainsPath(upperBoundInclusive, allFallingBytes.Take(i + 1).ToFrozenSet());
            if (currentContainsPath && !nextContainsPath)
                return allFallingBytes[i];
            if (currentContainsPath)
                low = i + 1;
            else if (!nextContainsPath)
                highInclusive = i - 1;
        }

        throw new InvalidOperationException();
    }

    private static bool ContainsPath(int upperBoundInclusive, IReadOnlySet<V> fallingBytes)
    {
        Graph graph = new(upperBoundInclusive + 1, fallingBytes);
        var source = V.Zero;
        var steps = EnumerableBfs<V>.EnumerateEdges(graph, source);
        var destination = Vector128.Create(upperBoundInclusive, upperBoundInclusive, 0, 0);
        return steps.Any(step => step.Head == destination);
    }
}
