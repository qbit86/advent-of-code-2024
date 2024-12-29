using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2024;

public sealed class Solver
{
    private readonly Dictionary<Node, long> _cache = [];
    private readonly int _depth;

    public Solver(int depth)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(depth);
        _depth = depth;
    }

    public long Solve(string code)
    {
        var endpointsCollection = code.Prepend('A').Zip(code);
        var buttonSequences =
            endpointsCollection.Select(it => Keypads.Numeric.GetSequence(it.First, it.Second)).ToList();
        return buttonSequences.Select(Selector).Sum();

        long Selector(string buttonSequence) => RetrieveOrCompute(new(buttonSequence, _depth));
    }

    private long RetrieveOrCompute(Node node)
    {
        if (_cache.TryGetValue(node, out long cachedResult))
            return cachedResult;
        long result = Compute(node);
        _cache.Add(node, result);
        return result;
    }

    private long Compute(Node node)
    {
        if (node.Depth is 0)
            return node.ButtonSequence.Length;
        var endpointsCollection = node.ButtonSequence.Prepend('A').Zip(node.ButtonSequence);
        var buttonSequences =
            endpointsCollection.Select(it => Keypads.Directional.GetSequence(it.First, it.Second)).ToList();
        return buttonSequences.Select(Selector).Sum();

        long Selector(string buttonSequence) => RetrieveOrCompute(new(buttonSequence, node.Depth - 1));
    }

    private readonly record struct Node(string ButtonSequence, int Depth);
}
