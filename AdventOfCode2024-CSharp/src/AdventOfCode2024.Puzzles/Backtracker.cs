using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static AdventOfCode2024.TryHelpers;

namespace AdventOfCode2024;

internal sealed class Backtracker
{
    private readonly Information _info;
    private readonly PackedList _program;
    private readonly ILookup<long, long> _registersByOutput;
    private readonly StatelessSimulator _simulator;

    private Backtracker(
        Information info, StatelessSimulator simulator, PackedList program, ILookup<long, long> registersByOutput)
    {
        _info = info;
        _simulator = simulator;
        _program = program;
        _registersByOutput = registersByOutput;
    }

    internal static Backtracker Create(Information info)
    {
        StatelessSimulator simulator = new(info.Program);
        var registerOutputPairs = EnumerateRegisterOutputPairs(info, simulator);
        var registersByOutput = registerOutputPairs.ToLookup(it => it.Value, it => it.Key);
        var program = info.Program.Select(Convert.ToInt64)
            .Aggregate(PackedList.Empty, (current, entry) => current.Add(entry));
        return new(info, simulator, program, registersByOutput);
    }

    internal long Solve()
    {
        Node root = new(0, default);
        List<long> solutions = [];
        _ = TryBacktrack(root, solutions, out _);
        return solutions.Min();
    }

    private bool TryBacktrack(Node node, List<long> solutions, out long result)
    {
        if (Accept(node))
        {
            solutions.Add(node.Register);
            return Some(node.Register, out result);
        }

        if (Reject(node))
            return None(node.Register, out result);
        long targetOutput = _program[node.Output.Count];
        var partialCandidates = _registersByOutput[targetOutput];
        foreach (long partialCandidate in partialCandidates)
        {
            int shiftAmount = PackedList.BitsPerItem * node.Output.Count;
            if (node.Output.Count > 0)
            {
                long shifted = node.Register >> shiftAmount;
                const long mask = 0b1_111_111;
                if (shifted != (partialCandidate & mask))
                    continue;
            }

            long registerCandidate = (partialCandidate << shiftAmount) | node.Register;
            Node candidate = new(registerCandidate, node.Output.Add(targetOutput));
            _ = TryBacktrack(candidate, solutions, out result);
        }

        return None(node.Register, out result);
    }

    private bool Accept(Node node)
    {
        if (node.Output != _program)
            return false;
        State initialState = new(node.Register, _info.B, _info.C, 0, PackedList.Empty);
        var finalState = _simulator.SimulateOutputs(initialState, node.Output.Count + 1);
        return finalState.Output == _program;
    }

    private bool Reject(Node node) => node.Output.Count >= _program.Count;

    private static IEnumerable<KeyValuePair<long, long>> EnumerateRegisterOutputPairs(Information info,
        StatelessSimulator simulator)
    {
        const long upperBound = 1L << 10;
        for (long a = 0; a < upperBound; ++a)
        {
            State initialState = new(a, info.B, info.C, 0, PackedList.Empty);
            if (!simulator.TrySimulateSingleOutput(initialState, out var finalState))
                continue;
            Debug.Assert(finalState.Output.Count is 1);
            long singleOutput = finalState.Output[0];
            yield return new(a, singleOutput);
        }
    }

    private readonly record struct Node(long Register, PackedList Output);
}
