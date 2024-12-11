using System;
using System.Diagnostics;

namespace AdventOfCode2024;

internal static class Program
{
    private static void Main()
    {
        long startingTimestamp = Stopwatch.GetTimestamp();
        long answer = SolveSingle();
        var elapsed = Stopwatch.GetElapsedTime(startingTimestamp);
        Console.WriteLine(answer);
        Console.WriteLine(elapsed);
    }

    private static long SolveSingle()
    {
        const string path = "input.txt";
        try
        {
            return PartTwoPuzzle.Solve(path);
        }
        catch (NotImplementedException)
        {
            return PartOnePuzzle.Solve(path);
        }
    }
}
