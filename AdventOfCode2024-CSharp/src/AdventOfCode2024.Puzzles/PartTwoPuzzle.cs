using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2024;

public static class PartTwoPuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path, Encoding.UTF8);
        return Solve(lines);
    }

    private static long Solve<TRows>(TRows rows)
        where TRows : IReadOnlyList<string> => throw new NotImplementedException();
}
