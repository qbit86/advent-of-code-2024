using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

using V = Vector128<long>;

public readonly partial record struct RobotState(V Position, V Velocity)
{
    private const string Pattern = @"(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)";
    private const RegexOptions Options = RegexOptions.Compiled | RegexOptions.CultureInvariant;

    public static RobotState Parse(string line)
    {
        ArgumentNullException.ThrowIfNull(line);

        var matches = CreateRegex().Matches(line);
        var groups = matches.Single().Groups;
        long px = long.Parse(groups[1].Value, CultureInfo.InvariantCulture);
        long py = long.Parse(groups[2].Value, CultureInfo.InvariantCulture);
        long vx = long.Parse(groups[3].Value, CultureInfo.InvariantCulture);
        long vy = long.Parse(groups[4].Value, CultureInfo.InvariantCulture);
        var position = Vector128.Create(px, py);
        var velocity = Vector128.Create(vx, vy);
        return new(position, velocity);
    }

    [GeneratedRegex(Pattern, Options)]
    private static partial Regex CreateRegex();
}
