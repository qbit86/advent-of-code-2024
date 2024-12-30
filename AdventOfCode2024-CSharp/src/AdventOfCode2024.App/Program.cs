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
        return PartOnePuzzle.Solve(path);
    }
}
