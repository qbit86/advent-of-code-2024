using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using P = Vector128<int>;

internal static class Helpers
{
    internal static P Create(int element0, int element1) => Vector128.Create(element0, element1, 0, 0);
}
