using System.Collections.Generic;
using System.Runtime.Intrinsics;
using Arborescence;
using Arborescence.Models;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal sealed class PerimeterGraph :
    IOutNeighborsAdjacency<V, IEnumerator<V>>,
    IOutEdgesIncidence<V, IncidenceEnumerator<V, IEnumerator<V>>>
{
    private readonly Grid _grid;
    private readonly char _kind;

    public PerimeterGraph(Grid grid, char kind)
    {
        _grid = grid;
        _kind = kind;
    }

    public IncidenceEnumerator<V, IEnumerator<V>> EnumerateOutEdges(V vertex) =>
        IncidenceEnumerator.Create(vertex, EnumerateOutNeighbors(vertex));

    public IEnumerator<V> EnumerateOutNeighbors(V vertex)
    {
        if (!_grid.TryGetValue(vertex, out char kind) || kind != _kind)
            yield break;
        foreach (var direction in Helpers.Directions)
            yield return vertex + direction;
    }
}
