using System;
using System.Linq;

namespace AdventOfCode2024;

internal readonly record struct Input(string[] Patterns, string[] Designs)
{
    internal static Input Create(string[] lines)
    {
        string[] patterns = [.. lines[0].Split(", ", StringSplitOptions.TrimEntries)];
        ArgumentOutOfRangeException.ThrowIfNotEqual(lines[1], string.Empty);
        string[] designs = lines.Skip(2).ToArray();
        return new(patterns, designs);
    }
}
