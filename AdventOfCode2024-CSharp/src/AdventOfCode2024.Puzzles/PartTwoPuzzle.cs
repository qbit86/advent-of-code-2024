using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AdventOfCode2024;

public static class PartTwoPuzzle
{
    public static long SolveNaiveLong(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return SolveNaive(lines);
    }

    public static string Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines).ToString(CultureInfo.InvariantCulture);
    }

    public static long SolveLong(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long SolveNaive(string[] lines)
    {
        var info = Information.Create(lines);
        var chunks = info.Program.Chunk(2);
        int[] advChunk = chunks.Single(it => it[0] is 0);
        int shiftAmount = advChunk[1];
        int iterationCount = info.Program.Count;
        long lowerBound = checked(1L << (shiftAmount * (iterationCount - 1)));
        long upperBound = lowerBound << shiftAmount;
        long step = 1 << shiftAmount;
        for (long candidate = lowerBound; candidate < upperBound; candidate += step)
        {
            if (MatchesOutput(info, candidate))
                return candidate;
        }

        throw new InvalidOperationException();
    }

    private static long Solve(string[] lines)
    {
        var info = Information.Create(lines);
        var backtracker = Backtracker.Create(info);
        return backtracker.Solve();
    }

    private static bool MatchesOutput(Information info, long candidate)
    {
        State state = new(candidate, info.B, info.C, 0, PackedList.Empty);
        StatelessSimulator simulator = new(info.Program);
        while (simulator.TrySimulateSingleInstruction(state, out var nextState))
        {
            if (nextState.Output.Count > info.Program.Count)
                return false;

            if (nextState.Output.Count > 0)
            {
                int index = nextState.Output.Count - 1;
                if (nextState.Output[index] != info.Program[index])
                    return false;
            }

            state = nextState;
        }

        return true;
    }
}
