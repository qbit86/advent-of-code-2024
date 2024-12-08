using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using P = Vector128<int>;

public static class PartTwoPuzzle
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
                uint gcd = Gcd((uint)Math.Abs(direction[0]), (uint)Math.Abs(direction[1]));
                var step = direction / (int)gcd;
                PopulateAntinodeLocations(grid, antennaLocations[i], -step, antinodeLocations);
                PopulateAntinodeLocations(grid, antennaLocations[j], step, antinodeLocations);
            }
        }
    }

    private static void PopulateAntinodeLocations(
        Grid grid, P antennaLocation, Vector128<int> step, HashSet<P> antinodeLocations)
    {
        for (var p = antennaLocation;;)
        {
            if (!grid.Contains(p))
                return;
            _ = antinodeLocations.Add(p);
            p += step;
        }
    }

    private static uint Gcd(uint left, uint right)
    {
        while (right != 0)
        {
            uint temp = left % right;
            left = right;
            right = temp;
        }

        return left;
    }
}
