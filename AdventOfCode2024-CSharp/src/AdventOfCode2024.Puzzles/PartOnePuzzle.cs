using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AdventOfCode2024;

public static class PartOnePuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(string[] rows)
    {
        int[] seeds = rows.Select(it => int.Parse(it, CultureInfo.InvariantCulture)).ToArray();
        return Solve(seeds);
    }

    private static long Solve(int[] seeds) => seeds.Select(Solve2000).Sum();

    public static long Solve2000(int seed) => SolveSingleBuyer(seed, 2000);

    private static long SolveSingleBuyer(int seed, int iterationCount)
    {
        int secret = seed;
        for (int i = 0; i < iterationCount; ++i)
            secret = Helpers.SimulateSingleStep(secret);

        return secret;
    }
}
