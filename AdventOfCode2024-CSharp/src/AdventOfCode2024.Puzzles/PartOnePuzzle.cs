using System;
using System.IO;

namespace AdventOfCode2024;

public static class PartOnePuzzle
{
    public static string Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        var values = Solve(lines);
        return string.Join(',', values);
    }

    private static PackedList Solve(string[] lines)
    {
        var info = Information.Create(lines);
        State initialState = new(info.A, info.B, info.C, 0, PackedList.Empty);
        StatelessSimulator simulator = new(info.Program);
        var finalState = simulator.SimulateAllInstructions(initialState);
        return finalState.Output;
    }
}
