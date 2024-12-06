using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using static TryHelpers;
using V = Vector128<int>;

internal static class Helpers
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static V Create(int e0, int e1) => Vector128.Create(e0, e1, 0, 0);

    internal static V FindStartingPosition(string[] rows)
    {
        for (int rowIndex = 0; rowIndex < rows.Length; ++rowIndex)
        {
            string row = rows[rowIndex];
            int columnIndex = row.IndexOf('^', StringComparison.Ordinal);
            if (columnIndex >= 0)
                return Create(rowIndex, columnIndex);
        }

        throw new ArgumentException("No starting position could be found.", nameof(rows));
    }

    internal static void PopulateReachablePositions(Grid grid, Node startingNode, HashSet<V> positions)
    {
        var node = startingNode;
        while (TryGetNextNode(grid, node, out node))
            _ = positions.Add(node.Position);
    }

    private static bool TryGetNextNode(Grid grid, Node node, out Node nextNode)
    {
        var obstructionPosition = Vector128.Create(-1);
        return TryGetNextNode(grid, node, obstructionPosition, out nextNode);
    }

    internal static bool TryGetNextNode(Grid grid, Node node, V obstructionPosition, out Node nextNode)
    {
        var positionCandidate = node.Position + node.Direction;
        if (positionCandidate == obstructionPosition)
            return Some(Rotate(node), out nextNode);

        if (!grid.TryGetValue(positionCandidate, out char cellValue))
            return None(node with { Position = positionCandidate }, out nextNode);

        if (cellValue is not '#')
            return Some(node with { Position = positionCandidate }, out nextNode);

        return Some(Rotate(node), out nextNode);
    }

    private static Node Rotate(Node node) => node with { Direction = Rotate(node.Direction) };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static V Rotate(V direction)
    {
        var temp = Vector128.Shuffle(direction, Vector128.Create(1, 0, 2, 3));
        return temp * Vector128.Create(1, -1, 1, 1);
    }
}
