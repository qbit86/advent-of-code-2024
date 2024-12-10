using System.Collections.Generic;
using System.Runtime.Intrinsics;
using Arborescence;

namespace AdventOfCode2024;

using static Helpers;
using P = Vector128<int>;

internal sealed class Graph : IOutNeighborsAdjacency<P, IEnumerator<P>>
{
    private static readonly P[] s_directions = [Create(0, 1), Create(1, 0), Create(0, -1), Create(-1, 0)];

    private readonly Grid _grid;

    public Graph(Grid grid) => _grid = grid;

    public IEnumerator<P> EnumerateOutNeighbors(P vertex)
    {
        if (!_grid.TryGetHeight(vertex, out int height))
            yield break;

        foreach (var direction in s_directions)
        {
            var candidate = vertex + direction;
            if (_grid.TryGetHeight(candidate, out int neighborHeight) && neighborHeight == height + 1)
                yield return candidate;
        }
    }

    public IEnumerator<P> EnumerateInNeighbors(P vertex)
    {
        if (!_grid.TryGetHeight(vertex, out int height))
            yield break;

        foreach (var direction in s_directions)
        {
            var candidate = vertex + direction;
            if (_grid.TryGetHeight(candidate, out int neighborHeight) && neighborHeight == height - 1)
                yield return candidate;
        }
    }
}
