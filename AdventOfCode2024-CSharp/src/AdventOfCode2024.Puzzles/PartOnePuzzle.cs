using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2024;

public static class PartOnePuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path, Encoding.UTF8);
        return Solve(lines);
    }

    private static long Solve<TRows>(TRows rows)
        where TRows : IReadOnlyList<string>
    {
        (int[] leftNumbers, int[] rightNumbers) = Helpers.Parse(rows);

        Array.Sort(leftNumbers);
        Array.Sort(rightNumbers);

        var distances = leftNumbers.Zip(rightNumbers, (left, right) => long.Abs(left - right));
        return distances.Sum();
    }
}
