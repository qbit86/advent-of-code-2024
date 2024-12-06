using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Intrinsics;

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
        var startingPosition = Helpers.FindStartingPosition(rows);
        Node startingNode = new(startingPosition, Helpers.Create(-1, 0));
        HashSet<V> positions = [startingNode.Position];
        Helpers.PopulateReachablePositions(grid, startingNode, positions);
        return positions.Count;
    }
}
