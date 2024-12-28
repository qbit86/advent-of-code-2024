using System;
using System.IO;

namespace AdventOfCode2024;

public static class PartOnePuzzle
{
    public static long Solve(string path, int savedPicosecondsLowerBound)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return PartTwoPuzzle.Solve(lines, 2, savedPicosecondsLowerBound);
    }
}
