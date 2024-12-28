using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2024;

internal sealed class Worker
{
    private readonly string[] _patterns;

    internal Worker(string[] patterns) => _patterns = patterns;

    internal long Solve(string design)
    {
        List<(int Offset, int Length)> offsetLengthPairs = [];
        for (int i = 0; i < design.Length; ++i)
        {
            var designSubspan = design.AsSpan(i);
            foreach (string pattern in _patterns)
            {
                if (designSubspan.StartsWith(pattern))
                    offsetLengthPairs.Add(new(i, pattern.Length));
            }
        }

        var patternsByOffset = offsetLengthPairs.Distinct().ToLookup(it => it.Offset, it => it.Length);
        return Compute([], new(0));

        long GetFromCacheOrCompute(Dictionary<Node, long> cache, Node node)
        {
            if (cache.TryGetValue(node, out long result))
                return result;
            result = Compute(cache, node);
            cache.Add(node, result);
            return result;
        }

        long Compute(Dictionary<Node, long> cache, Node node)
        {
            if (node.Offset == design.Length)
                return 1;
            var patterns = patternsByOffset[node.Offset];
            return patterns.Select(pattern => GetFromCacheOrCompute(cache, new Node(node.Offset + pattern)))
                .Sum();
        }
    }
}

file readonly record struct Node(int Offset);
