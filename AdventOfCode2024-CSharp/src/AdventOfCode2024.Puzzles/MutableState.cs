using System.Collections.Generic;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal readonly record struct MutableState(
    V RobotPosition,
    IList<V> BoxPositionById,
    IDictionary<V, int> BoxIdByPosition);
