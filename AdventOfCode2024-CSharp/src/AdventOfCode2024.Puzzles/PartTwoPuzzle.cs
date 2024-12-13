using System;
using System.Linq;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

public static class PartTwoPuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        var descriptions = Helpers.Parse(path);
        var fixedDescriptions = descriptions.Select(Fix).ToArray();
        return Helpers.ComputeTokens(fixedDescriptions);
    }

    private static Description Fix(Description description)
    {
        var addend = Vector128.Create(10000000000000L);
        var prize = description.Prize + addend;
        return description with { Prize = prize };
    }
}
