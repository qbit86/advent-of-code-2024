using System;
using System.Globalization;
using System.Runtime.Intrinsics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

using V = Vector128<long>;

internal sealed partial record Description(V ADelta, V BDelta, V Prize)
{
    private const string Pattern = @"\d+";
    private const RegexOptions Options = RegexOptions.Compiled | RegexOptions.CultureInvariant;

    private static readonly string[] s_separators = ["\n", "\r\n"];

    private static Regex? s_parsingRegex;

    private static Regex ParsingRegex => s_parsingRegex ??= CreateRegex();

    internal static Description Parse(string s)
    {
        string[] lines = s.Split(s_separators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var aDelta = ParseLine(lines[0]);
        var bDelta = ParseLine(lines[1]);
        var prize = ParseLine(lines[2]);
        return new(aDelta, bDelta, prize);
    }

    private static V ParseLine(string line)
    {
        var matches = ParsingRegex.Matches(line);
        long x = long.Parse(matches[0].ValueSpan, CultureInfo.InvariantCulture);
        long y = long.Parse(matches[1].ValueSpan, CultureInfo.InvariantCulture);
        return Vector128.Create(x, y);
    }

    [GeneratedRegex(Pattern, Options)]
    private static partial Regex CreateRegex();
}
