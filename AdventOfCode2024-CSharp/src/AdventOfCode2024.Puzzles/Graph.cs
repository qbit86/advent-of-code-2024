using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using Arborescence;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal sealed class Graph : IOutNeighborsAdjacency<V, IEnumerator<V>>
{
    private readonly IReadOnlySet<V> _fallingBytes;
    private readonly V _upperBound;

    public Graph(int upperBound, IReadOnlySet<V> fallingBytes)
    {
        _upperBound = Vector128.Create(upperBound);
        _fallingBytes = fallingBytes;
    }

    public IEnumerator<V> EnumerateOutNeighbors(V vertex)
    {
        var neighborCandidates = Helpers.Directions.Select(it => vertex + it);
        return neighborCandidates.Where(IsWalkable).GetEnumerator();
    }

    private bool IsWalkable(V vertex)
    {
        if (Vector128.LessThanAny(vertex, V.Zero))
            return false;

        if (Vector128.GreaterThanOrEqualAny(vertex, _upperBound))
            return false;

        return !_fallingBytes.Contains(vertex);
    }
}
