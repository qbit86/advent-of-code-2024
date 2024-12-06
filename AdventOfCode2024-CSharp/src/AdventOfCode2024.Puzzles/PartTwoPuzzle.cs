using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

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
        var startingPosition = Helpers.FindStartingPosition(rows);
        Node startingNode = new(startingPosition, Helpers.Create(-1, 0));
        HashSet<V> obstructionPositionCandidates = [];
        Helpers.PopulateReachablePositions(grid, startingNode, obstructionPositionCandidates);
        _ = obstructionPositionCandidates.Remove(startingPosition);
        return obstructionPositionCandidates.Count(HasLoopForObstacle);

        bool HasLoopForObstacle(V p) => HasLoop(grid, startingNode, p);
    }

    private static bool HasLoop(Grid grid, Node startingNode, V obstructionPosition)
    {
        HashSet<Node> nodes = [startingNode];
        var node = startingNode;
        while (Helpers.TryGetNextNode(grid, node, obstructionPosition, out node))
        {
            if (nodes.Contains(node))
                return true;
            _ = nodes.Add(node);
        }

        return false;
    }
}
