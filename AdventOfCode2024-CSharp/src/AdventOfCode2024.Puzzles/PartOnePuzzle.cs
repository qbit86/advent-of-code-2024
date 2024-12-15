using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics.Tensors;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<long>;

public static class PartOnePuzzle
{
    public static long Solve(string path, V size)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines, size);
    }

    private static long Solve(string[] rows, V size)
    {
        const int seconds = 100;
        var robotStates = rows.Select(RobotState.Parse).ToArray();
        var positions = robotStates.Select(it => Helpers.Mod(it.Position + seconds * it.Velocity, size)).ToArray();
        long[] robotsByQuadrant = new long[4];
        var middle = size >> 1;
        var middlePlusOne = middle + V.One;
        foreach (var position in positions)
        {
            if (Vector128.EqualsAny(position, middle))
                continue;
            var indexVector = position / middlePlusOne;
            int index = (int)Vector128.Sum(indexVector * Vector128.Create(2L, 1L));
            Debug.Assert(index >= 0 && index < robotsByQuadrant.Length,
                $"{nameof(index)}: {index}, {nameof(position)}: {position}");
            robotsByQuadrant[index] += 1;
        }

        return TensorPrimitives.Product<long>(robotsByQuadrant);
    }
}
