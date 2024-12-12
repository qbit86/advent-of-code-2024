using System.Collections.Generic;
using System.Runtime.Intrinsics;
using Arborescence;
using Arborescence.Models;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal sealed class RegionGraph :
    IOutNeighborsAdjacency<V, IEnumerator<V>>,
    IOutEdgesIncidence<V, IncidenceEnumerator<V, IEnumerator<V>>>
{
    private readonly Grid _grid;

    internal RegionGraph(Grid grid) => _grid = grid;

    public IncidenceEnumerator<V, IEnumerator<V>> EnumerateOutEdges(V vertex) =>
        IncidenceEnumerator.Create(vertex, EnumerateOutNeighbors(vertex));

    public IEnumerator<V> EnumerateOutNeighbors(V vertex)
    {
        if (!_grid.TryGetValue(vertex, out char kind))
            yield break;
        foreach (var direction in Helpers.Directions)
        {
            var neighborCandidate = vertex + direction;
            if (!_grid.TryGetValue(neighborCandidate, out char neighborKind))
                continue;
            if (kind == neighborKind)
                yield return neighborCandidate;
        }
    }
}
