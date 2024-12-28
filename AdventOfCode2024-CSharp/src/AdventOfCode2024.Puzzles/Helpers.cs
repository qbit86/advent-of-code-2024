using System.Collections.Frozen;
using System.Collections.Generic;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal static class Helpers
{
    internal static FrozenDictionary<char, V> DirectionByInstruction { get; } = CreateDirectionByInstruction();

    private static FrozenDictionary<char, V> CreateDirectionByInstruction()
    {
        KeyValuePair<char, V>[] pairs =
        [
            new('>', Vector128.Create(0, 1, 0, 0)),
            new('v', Vector128.Create(1, 0, 0, 0)),
            new('<', Vector128.Create(0, -1, 0, 0)),
            new('^', Vector128.Create(-1, 0, 0, 0))
        ];
        return pairs.ToFrozenDictionary();
    }

    internal static long ComputeGpsCoordinate(V position)
    {
        var product = position * Vector128.Create(100, 1, 0, 0);
        return Vector128.Sum(product);
    }
}
