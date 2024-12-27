using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("sample-2.txt", 117440L)]
    internal void SolveNaive_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartTwoPuzzle.SolveNaiveLong(inputPath);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("sample-2.txt")]
    internal void SolveNaive_ShouldRoundtrip(string inputPath)
    {
        long actual = PartTwoPuzzle.SolveNaiveLong(inputPath);
        string[] lines = File.ReadAllLines(inputPath);
        var info = Information.Create(lines);
        var finalOutput = GetOutput(info, actual);
        Assert.Equal(info.Program.Select(it => (long)it), finalOutput);
    }

    [Theory]
    [InlineData("input.txt")]
    internal void Solve_ShouldRoundtrip(string inputPath)
    {
        long actual = PartTwoPuzzle.SolveLong(inputPath);
        string[] lines = File.ReadAllLines(inputPath);
        var info = Information.Create(lines);
        var finalOutput = GetOutput(info, actual);
        long[] program = info.Program.Select(Convert.ToInt64).ToArray();
        Assert.Equal(program, finalOutput);
    }

    [Theory]
    [InlineData("input.txt", 37221261688308L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartTwoPuzzle.SolveLong(inputPath);
        Assert.Equal(expected, actual);
    }

    private static PackedList GetOutput(Information info, long initialA)
    {
        State initialState = new(initialA, info.B, info.C, 0, PackedList.Empty);
        StatelessSimulator simulator = new(info.Program);
        var finalState = simulator.SimulateAllInstructions(initialState);
        return finalState.Output;
    }
}
