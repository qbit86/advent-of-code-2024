using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

    private static long Solve(string[] rows)
    {
        var reports = Helpers.Parse(rows);
        return reports.Count(IsReportWeaklySafe);
    }

    public static bool IsReportWeaklySafe<TLevels>(TLevels levels)
        where TLevels : IReadOnlyList<int>
    {
        if (levels.Count <= 2)
            return true;

        var report = Report.Create(levels, int.MinValue);
        for (int i = 0; i < levels.Count; ++i)
        {
            bool isSafeLeft = report.IsLevelSafeLeft(i, out int leftSign);
            bool isSafeRight = report.IsLevelSafeRight(i, out int rightSign);
            bool isSameSign = leftSign is 0 || rightSign is 0 || leftSign == rightSign;
            bool isSafe = isSafeLeft && isSafeRight && isSameSign;
            if (!isSafe)
            {
                return IsReportStrictlySafe(levels, i - 2, i - 1) ||
                    IsReportStrictlySafe(levels, i - 1, i) ||
                    IsReportStrictlySafe(levels, i, i + 1);
            }
        }

        return true;
    }

    private static bool IsReportStrictlySafe<TLevels>(TLevels levels, int start, int excludedIndex)
        where TLevels : IReadOnlyList<int> =>
        IsReportStrictlySafeUnchecked(levels, Math.Max(start, 0), excludedIndex);

    private static bool IsReportStrictlySafeUnchecked<TLevels>(TLevels levels, int start, int excludedIndex)
        where TLevels : IReadOnlyList<int>
    {
        Debug.Assert(start >= 0);
        int count = levels.Count - start;
        if (count <= 1)
            return true;

        var report = Report.Create(levels, excludedIndex);
        for (int i = start; i < levels.Count; ++i)
        {
            if (i == excludedIndex)
                continue;
            bool isSafeLeft = report.IsLevelSafeLeft(i, out int leftSign);
            bool isSafeRight = report.IsLevelSafeRight(i, out int rightSign);
            bool isSameSign = leftSign is 0 || rightSign is 0 || leftSign == rightSign;
            bool isSafe = isSafeLeft && isSafeRight && isSameSign;
            if (!isSafe)
                return false;
        }

        return true;
    }
}
