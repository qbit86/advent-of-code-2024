using System.Buffers;
using System.Collections.Generic;
using System.Runtime.Intrinsics;
using Arborescence;
using Arborescence.Models;

namespace AdventOfCode2024;

using Node = Endpoints<Vector128<int>>;

internal sealed class MetaGraph : IOutNeighborsAdjacency<Node, IEnumerator<Node>>,
    IOutEdgesIncidence<Node, IncidenceEnumerator<Node, IEnumerator<Node>>>
{
    private readonly IReadOnlySet<Node> _nodes;

    public MetaGraph(IReadOnlySet<Node> nodes) => _nodes = nodes;

    public IncidenceEnumerator<Node, IEnumerator<Node>> EnumerateOutEdges(Node vertex) =>
        IncidenceEnumerator.Create(vertex, EnumerateOutNeighbors(vertex));

    public IEnumerator<Node> EnumerateOutNeighbors(Node vertex)
    {
        var vertexDirection = vertex.Head - vertex.Tail;
        var directions = ArrayPool<Vector128<int>>.Shared.Rent(2);
        directions[0] = Rotate(vertexDirection);
        directions[1] = Rotate(-vertexDirection);
        for (int i = 0; i < 2; ++i)
        {
            var direction = directions[i];
            var neighborCandidateTail = vertex.Tail + direction;
            var neighborCandidate = Endpoints.Create(neighborCandidateTail, vertex.Head + direction);
            if (_nodes.Contains(neighborCandidate))
                yield return neighborCandidate;
        }

        ArrayPool<Vector128<int>>.Shared.Return(directions);
    }

    private static Vector128<int> Rotate(Vector128<int> direction)
    {
        var temp = Vector128.Shuffle(direction, Vector128.Create(1, 0, 2, 3));
        return temp * Vector128.Create(1, -1, 1, 1);
    }
}
