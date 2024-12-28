using System.Collections.Immutable;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal readonly record struct ImmutableState(V RobotPosition, ImmutableHashSet<V> BoxPositions);
