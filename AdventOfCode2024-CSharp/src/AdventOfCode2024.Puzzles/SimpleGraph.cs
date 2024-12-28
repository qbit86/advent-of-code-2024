using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Intrinsics;
using Arborescence;
using Arborescence.Models;
using JetBrains.Annotations;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal sealed class SimpleGraph :
    IOutNeighborsAdjacency<V, IEnumerator<V>>,
    IOutEdgesIncidence<V, IncidenceEnumerator<V, IEnumerator<V>>>,
    IHeadIncidence<V, Endpoints<V>>
{
    private readonly Grid _grid;

    internal SimpleGraph(Grid grid) => _grid = grid;

    public bool TryGetHead(Endpoints<V> edge, [UnscopedRef] out V head)
    {
        head = edge.Head;
        return _grid.IsWalkable(head);
    }

    public IncidenceEnumerator<V, IEnumerator<V>> EnumerateOutEdges(V vertex) =>
        IncidenceEnumerator.Create(vertex, EnumerateOutNeighbors(vertex));

    [MustDisposeResource]
    public IEnumerator<V> EnumerateOutNeighbors(V vertex)
    {
        var neighborCandidates = Helpers.Directions.Select(it => vertex + it);
        return neighborCandidates.Where(_grid.IsWalkable).GetEnumerator();
    }
}
