using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2024;

public static class PartTwoPuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(string[] rows)
    {
        var input = Input.Create(rows);
        Worker worker = new(input.Patterns);
        long result = input.Designs.Sum(it => worker.Solve(it));
        return result;
    }
}
