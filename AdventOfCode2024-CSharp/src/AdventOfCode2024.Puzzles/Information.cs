using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdventOfCode2024;

public readonly record struct Information(int A, int B, int C, IReadOnlyList<int> Program)
{
    public static Information Create(string[] lines)
    {
        ArgumentNullException.ThrowIfNull(lines);
        var options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
        int a = int.Parse(lines[0].Split(": ", options)[1], CultureInfo.InvariantCulture);
        int b = int.Parse(lines[1].Split(": ", options)[1], CultureInfo.InvariantCulture);
        int c = int.Parse(lines[2].Split(": ", options)[1], CultureInfo.InvariantCulture);
        string[] parts = lines[4].Split(["Program: ", ","], options);
        int[] program = parts.Select(it => int.Parse(it, CultureInfo.InvariantCulture)).ToArray();
        return new(a, b, c, program);
    }
}
