using System;
using System.Diagnostics;

namespace AdventOfCode2024;

internal static class Program
{
    private static void Main()
    {
        long startingTime = Stopwatch.GetTimestamp();
        string answer = SolveSingle();
        var elapsed = Stopwatch.GetElapsedTime(startingTime);
        Console.WriteLine(answer);
        Console.WriteLine(elapsed);
    }

    private static string SolveSingle()
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
