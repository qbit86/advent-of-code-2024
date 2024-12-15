using System;
using System.Runtime.Intrinsics;

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
            return PartTwoPuzzle.Solve(path);
        }
        catch (NotImplementedException)
        {
            return PartOnePuzzle.Solve(path, Vector128.Create(101L, 103L));
        }
    }
}
