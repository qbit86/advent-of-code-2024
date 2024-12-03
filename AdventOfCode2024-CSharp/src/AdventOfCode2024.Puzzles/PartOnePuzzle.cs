using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public static partial class PartOnePuzzle
{
    private const string Pattern = @"mul\((\d{1,3}),(\d{1,3})\)";

    private static Regex? s_regex;

    private static Regex MulRegex => s_regex ??= CreateRegex();

    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(IReadOnlyList<string> rows) => rows.Select(SolveSingle).Sum();

    private static long SolveSingle(string line)
    {
        long sum = 0L;
        var matches = MulRegex.Matches(line);
        for (int matchIndex = 0; matchIndex < matches.Count; ++matchIndex)
        {
            var match = matches[matchIndex];
            var groups = match.Groups;
            int left = int.Parse(groups[1].Captures[0].ValueSpan, CultureInfo.InvariantCulture);
            int right = int.Parse(groups[2].Captures[0].ValueSpan, CultureInfo.InvariantCulture);
            sum += Math.BigMul(left, right);
        }

        return sum;
    }

    [GeneratedRegex(Pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex CreateRegex();
}
