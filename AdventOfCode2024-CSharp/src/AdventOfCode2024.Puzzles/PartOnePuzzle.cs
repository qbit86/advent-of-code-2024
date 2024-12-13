using System;

namespace AdventOfCode2024;

public static class PartOnePuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        var descriptions = Helpers.Parse(path);
        return Helpers.ComputeTokens(descriptions);
    }
}
