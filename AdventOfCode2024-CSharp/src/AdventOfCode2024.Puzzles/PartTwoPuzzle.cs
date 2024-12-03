using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public static partial class PartTwoPuzzle
{
    private const string Pattern = @"(mul\((\d{1,3}),(\d{1,3})\))|(do\(\))|(don't\(\))";

    private static Regex? s_regex;

    private static Regex ConditionalMulRegex => s_regex ??= CreateRegex();

    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(IReadOnlyList<string> rows)
    {
        List<IInstruction> instructions = [];
        foreach (string line in rows)
            PopulateInstructions(line, instructions);
        return Solve(instructions);
    }

    private static long Solve(List<IInstruction> instructions)
    {
        long sum = 0L;
        bool shouldAdd = true;
        foreach (var instruction in instructions)
        {
            switch (instruction)
            {
                case DoInstruction:
                    shouldAdd = true;
                    continue;
                case DontInstruction:
                    shouldAdd = false;
                    continue;
                case MulInstruction m:
                    if (!shouldAdd)
                        continue;
                    int left = int.Parse(m.Left.Captures[0].ValueSpan, CultureInfo.InvariantCulture);
                    int right = int.Parse(m.Right.Captures[0].ValueSpan, CultureInfo.InvariantCulture);
                    sum += Math.BigMul(left, right);
                    break;
                default:
                    throw new UnreachableException();
            }
        }

        return sum;
    }

    private static void PopulateInstructions(string line, List<IInstruction> instructions)
    {
        var matches = ConditionalMulRegex.Matches(line);
        for (int matchIndex = 0; matchIndex < matches.Count; ++matchIndex)
        {
            var match = matches[matchIndex];
            if (match.Value is "do()")
            {
                instructions.Add(DoInstruction.Instance);
                continue;
            }

            if (match.Value is "don't()")
            {
                instructions.Add(DontInstruction.Instance);
                continue;
            }

            var groups = match.Groups;
            instructions.Add(new MulInstruction(groups[2], groups[3]));
        }
    }

    [GeneratedRegex(Pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex CreateRegex();
}
