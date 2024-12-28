using System;

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
        try
        {
            return PartTwoPuzzle.Solve(path, 20, 100);
        }
        catch (NotImplementedException)
        {
            return PartOnePuzzle.Solve(path, 100);
        }
    }
}
