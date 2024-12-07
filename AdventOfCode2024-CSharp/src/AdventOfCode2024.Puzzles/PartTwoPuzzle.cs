using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Arborescence;
using Arborescence.Traversal.Adjacency;

namespace AdventOfCode2024;

public static class PartTwoPuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(IReadOnlyList<string> rows)
    {
        var equations = rows.Select(Equation.Parse);
        return equations.Where(CanBeTrue).Sum(it => it.TestValue);
    }

    private static bool CanBeTrue(Equation equation)
    {
        Node source = new(equation.Numbers[0], 0, []);
        Graph graph = new(equation);
        var nodes = EnumerableDfs<Node>.EnumerateVertices(graph, source);
        int lastIndexInclusive = equation.Numbers.Length - 1;
        return nodes.Any(it => it.RunningTotalInclusive == equation.TestValue && it.Index == lastIndexInclusive);
    }
}

file sealed class Graph : IOutNeighborsAdjacency<Node, IEnumerator<Node>>
{
    private readonly Equation _equation;

    public Graph(Equation equation) => _equation = equation;

    public IEnumerator<Node> EnumerateOutNeighbors(Node vertex) => EnumerateInNeighborsIterator(vertex);

    private IEnumerator<Node> EnumerateInNeighborsIterator(Node node)
    {
        if (node.RunningTotalInclusive > _equation.TestValue)
            yield break;

        int neighborIndex = node.Index + 1;
        if (neighborIndex >= _equation.Numbers.Length)
            yield break;

        int number = _equation.Numbers[neighborIndex];
        long inclusiveRunningTotal = ConcatenateDecimalDigits(node.RunningTotalInclusive, number);
        yield return new(inclusiveRunningTotal, neighborIndex, []);
        yield return new(node.RunningTotalInclusive * number, neighborIndex, []);
        yield return new(node.RunningTotalInclusive + number, neighborIndex, []);
    }

    private static long ConcatenateDecimalDigits(long left, int right)
    {
        int rightDigitCount = ComputeDigitCount(right);
        long factor = (long)Math.Pow(10, rightDigitCount);
        return left * factor + right;
    }

    private static int ComputeDigitCount(int n)
    {
        if (n is 0)
            return 1;

        int m = Math.Abs(n);
        return (int)Math.Floor(Math.Log10(m)) + 1;
    }
}
