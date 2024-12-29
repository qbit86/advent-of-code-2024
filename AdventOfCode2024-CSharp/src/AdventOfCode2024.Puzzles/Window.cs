using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

public readonly record struct Window(int Index, int Price, Vector128<int> Changes);
