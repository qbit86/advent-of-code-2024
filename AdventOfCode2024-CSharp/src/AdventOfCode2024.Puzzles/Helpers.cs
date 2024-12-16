using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal static class Helpers
{
    internal static V Create(int e0, int e1) => Vector128.Create(e0, e1, 0, 0);

    internal static V RotatePositive(V direction)
    {
        var reordered = Vector128.Shuffle(direction, Vector128.Create(1, 0, 2, 3));
        return reordered * Vector128.Create(1, -1, 1, 1);
    }

    internal static V RotateNegative(V direction)
    {
        var reordered = Vector128.Shuffle(direction, Vector128.Create(1, 0, 2, 3));
        return reordered * Vector128.Create(-1, 1, 1, 1);
    }
}
