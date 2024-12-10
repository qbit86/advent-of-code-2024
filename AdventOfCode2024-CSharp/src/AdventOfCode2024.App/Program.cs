using System;
using System.Diagnostics;

namespace AdventOfCode2024;

internal static class Program
{
    private static void Main()
    {
        long startingTimestamp = Stopwatch.GetTimestamp();
        long answer = SolveSingle();
        Console.WriteLine(answer);
        Console.WriteLine(Stopwatch.GetElapsedTime(startingTimestamp));
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
