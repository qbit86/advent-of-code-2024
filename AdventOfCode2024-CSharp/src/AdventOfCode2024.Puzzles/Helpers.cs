using System.Collections.Immutable;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal static class Helpers
{
    internal static ImmutableArray<V> Directions { get; } = [Create(0, 1), Create(1, 0), Create(0, -1), Create(-1, 0)];

    internal static V Create(int e0, int e1) => Vector128.Create(e0, e1, 0, 0);
}
