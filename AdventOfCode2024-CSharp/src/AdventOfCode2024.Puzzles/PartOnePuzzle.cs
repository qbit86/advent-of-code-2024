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

    private static long Solve(string[] rows)
    {
        var reports = Helpers.Parse(rows);
        return reports.Count(IsReportSafe);
    }

    private static bool IsReportSafe<TLevels>(TLevels levels)
        where TLevels : IReadOnlyList<int>
    {
        if (levels.Count <= 1)
            return true;
        int[] differences = levels.Zip(levels.Skip(1), (a, b) => b - a).ToArray();
        if (!IsDifferenceSafe(differences[0]))
            return false;
        if (differences.Length is 1)
            return true;
        int sign = Math.Sign(differences[0]);
        return differences.Skip(1).All(it => Math.Sign(it) == sign && IsDifferenceSafe(it));
    }

    private static bool IsDifferenceSafe(int difference)
    {
        int d = Math.Abs(difference);
        return d is >= 1 and <= 3;
    }
}
