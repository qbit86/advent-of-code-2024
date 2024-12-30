using System;
using Arborescence;

namespace AdventOfCode2024;

internal static class Helpers
{
    internal static Endpoints<string> ParseEdge(string line)
    {
        string[] parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (parts.Length is not 2)
            throw new FormatException("Invalid edge format: " + line);
        return new Endpoints<string>(parts[0], parts[1]);
    }

    internal static Endpoints<string> ReverseEdge(Endpoints<string> edge) => new(edge.Head, edge.Tail);
}
