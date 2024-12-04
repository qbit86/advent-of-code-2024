using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2024;

public static class PartTwoPuzzle
{
    private const string Pattern = "MS";
    private const string ReversedPattern = "SM";
    private const int WindowSize = 3;

    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve<TRows>(TRows rows)
        where TRows : IReadOnlyList<string>
    {
        int count = 0;
        int horizontalBound = rows.Count - WindowSize + 1;
        for (int i = 0; i < horizontalBound; ++i)
        {
            int verticalBound = rows[i].Length - WindowSize + 1;
            for (int j = 0; j < verticalBound; ++j)
                count += Convert.ToInt32(ContainsPattern(rows, i, j));
        }

        return count;
    }

    private static bool ContainsPattern<TRows>(TRows rows, int rowIndex, int columnIndex)
        where TRows : IReadOnlyList<string>
    {
        if (rows[rowIndex + 1][columnIndex + 1] is not 'A')
            return false;

        Span<char> span = stackalloc char[Pattern.Length];
        span[0] = rows[rowIndex][columnIndex];
        span[1] = rows[rowIndex + 2][columnIndex + 2];
        if (span is not Pattern and not ReversedPattern)
            return false;

        span[0] = rows[rowIndex + 2][columnIndex];
        span[1] = rows[rowIndex][columnIndex + 2];
        if (span is not Pattern and not ReversedPattern)
            return false;

        return true;
    }
}
