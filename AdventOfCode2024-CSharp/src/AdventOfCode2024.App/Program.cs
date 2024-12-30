using System;

namespace AdventOfCode2024;

internal static class Program
{
    private static void Main()
    {
        string answer = SolveSingle();
        Console.WriteLine(answer);
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
