using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using P = Vector128<int>;

public static class PartOnePuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(string[] lines)
    {
        Grid grid = new(lines);
        var antennaLocationsByFrequency = grid.Where(kv => kv.Value is not '.')
            .ToLookup(it => it.Value, it => it.Key);
        HashSet<P> antinodeLocations = [];
        foreach (var g in antennaLocationsByFrequency)
            PopulateAntinodeLocations(grid, [.. g], antinodeLocations);

        return antinodeLocations.Count;
    }

    private static void PopulateAntinodeLocations(
        Grid grid, IReadOnlyList<P> antennaLocations, HashSet<P> antinodeLocations)
    {
        for (int i = 0; i < antennaLocations.Count; ++i)
        {
            for (int j = 0; j < i; ++j)
            {
                var direction = antennaLocations[j] - antennaLocations[i];
                var firstAntinodeLocation = antennaLocations[i] - direction;
                if (grid.Contains(firstAntinodeLocation))
                    _ = antinodeLocations.Add(firstAntinodeLocation);
                var secondAntinodeLocation = antennaLocations[j] + direction;
                if (grid.Contains(secondAntinodeLocation))
                    _ = antinodeLocations.Add(secondAntinodeLocation);
            }
        }
    }
}
