using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics;
using Arborescence;
using Arborescence.Models;
using static AdventOfCode2024.Helpers;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal sealed class ReversedGraph : IOutNeighborsAdjacency<Node, IEnumerator<Node>>,
    IOutEdgesIncidence<Node, IncidenceEnumerator<Node, IEnumerator<Node>>>,
    IHeadIncidence<Node, Endpoints<Node>>
{
    private readonly Grid _grid;

    public ReversedGraph(Grid grid) => _grid = grid;

    public bool TryGetHead(Endpoints<Node> edge, [UnscopedRef] out Node head)
    {
        head = edge.Head;
        Debug.Assert(_grid.IsWalkable(head.Tile));
        return true;
    }

    public IncidenceEnumerator<Node, IEnumerator<Node>> EnumerateOutEdges(Node vertex) =>
        IncidenceEnumerator.Create(vertex, EnumerateOutNeighbors(vertex));

    public IEnumerator<Node> EnumerateOutNeighbors(Node vertex)
    {
        {
            var neighborTileCandidate = vertex.Tile - vertex.Direction;
            if (_grid.IsWalkable(neighborTileCandidate))
                yield return vertex with { Tile = neighborTileCandidate };
        }

        const int rotationCount = 2;
        var neighborDirectionCandidatesFromPool = ArrayPool<V>.Shared.Rent(rotationCount);
        neighborDirectionCandidatesFromPool[0] = RotateNegative(vertex.Direction);
        neighborDirectionCandidatesFromPool[1] = RotatePositive(vertex.Direction);
        for (int i = 0; i < rotationCount; ++i)
            yield return vertex with { Direction = neighborDirectionCandidatesFromPool[i] };

        ArrayPool<V>.Shared.Return(neighborDirectionCandidatesFromPool);
    }
}
