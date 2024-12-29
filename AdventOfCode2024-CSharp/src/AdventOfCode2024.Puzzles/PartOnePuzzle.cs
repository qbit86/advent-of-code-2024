using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static AdventOfCode2024.Helpers;

namespace AdventOfCode2024;

public static class PartOnePuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(string[] codes)
    {
        Solver solver = new(2);
        var pairs = codes.Select(it => new KeyValuePair<string, long>(it, solver.Solve(it)));
        var products = pairs.Select(it => GetNumericPart(it.Key) * it.Value);
        return products.Sum();
    }
}
