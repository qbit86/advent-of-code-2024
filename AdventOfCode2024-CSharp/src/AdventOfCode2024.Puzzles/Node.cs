using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal readonly record struct Node(V Position, V Direction);
