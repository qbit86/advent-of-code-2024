using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2024;

public static class PartOnePuzzle
{
    private const string Pattern = "XMAS";
    private const string ReversedPattern = "SAMX";
    private const int PatternLength = 4;

    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve<TRows>(TRows rows)
        where TRows : IReadOnlyList<string>
    {
        int horizontalCount = 0;
        int verticalCount = 0;
        int diagonalCount = 0;
        int antiDiagonalCount = 0;
        for (int i = 0; i < rows.Count; ++i)
        {
            for (int j = 0; j < rows[i].Length; ++j)
            {
                horizontalCount += Convert.ToInt32(HasHorizontal(rows, i, j));
                verticalCount += Convert.ToInt32(HasVertical(rows, i, j));
                diagonalCount += Convert.ToInt32(HasDiagonal(rows, i, j));
                antiDiagonalCount += Convert.ToInt32(HasAntiDiagonal(rows, i, j));
            }
        }

        return horizontalCount + verticalCount + diagonalCount + antiDiagonalCount;
    }

    private static bool HasHorizontal<TRows>(TRows rows, int rowIndex, int columnIndex)
        where TRows : IReadOnlyList<string>
    {
        string row = rows[rowIndex];
        if (columnIndex + PatternLength > row.Length)
            return false;
        var span = row.AsSpan(columnIndex, PatternLength);
        return span is Pattern || span is ReversedPattern;
    }

    private static bool HasVertical<TRows>(TRows rows, int rowIndex, int columnIndex)
        where TRows : IReadOnlyList<string>
    {
        if (rowIndex + PatternLength > rows.Count)
            return false;
        Span<char> span = stackalloc char[PatternLength];
        for (int i = 0; i < PatternLength; ++i)
            span[i] = rows[rowIndex + i][columnIndex];

        return span is Pattern || span is ReversedPattern;
    }

    private static bool HasDiagonal<TRows>(TRows rows, int rowIndex, int columnIndex)
        where TRows : IReadOnlyList<string>
    {
        if (columnIndex + PatternLength > rows[rowIndex].Length)
            return false;
        if (rowIndex + PatternLength > rows.Count)
            return false;
        Span<char> span = stackalloc char[PatternLength];
        for (int i = 0; i < PatternLength; ++i)
            span[i] = rows[rowIndex + i][columnIndex + i];

        return span is Pattern || span is ReversedPattern;
    }

    private static bool HasAntiDiagonal<TRows>(TRows rows, int rowIndex, int columnIndex)
        where TRows : IReadOnlyList<string>
    {
        if (columnIndex + PatternLength > rows[rowIndex].Length)
            return false;
        if (rowIndex + PatternLength > rows.Count)
            return false;
        Span<char> span = stackalloc char[PatternLength];
        for (int i = 0; i < PatternLength; ++i)
            span[i] = rows[rowIndex + PatternLength - 1 - i][columnIndex + i];

        return span is Pattern || span is ReversedPattern;
    }
}
