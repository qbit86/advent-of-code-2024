using System;
using System.Diagnostics;

namespace AdventOfCode2024;

internal static class Program
{
    private static void Main()
    {
        long answer = SolveSingle();
        Console.WriteLine(answer);
    }

    private static long SolveSingle()
    {
        const string path = "input.txt";
        long startingTime = Stopwatch.GetTimestamp();
        try
        {
            return PartTwoPuzzle.Solve(path);
        }
        catch (NotImplementedException)
        {
            return PartOnePuzzle.Solve(path);
        }
        finally
        {
            var elapsedTime = Stopwatch.GetElapsedTime(startingTime);
            Console.WriteLine(elapsedTime);
        }
    }
}
