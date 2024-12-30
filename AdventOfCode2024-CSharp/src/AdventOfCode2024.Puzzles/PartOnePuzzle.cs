using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AdventOfCode2024;

public static class PartOnePuzzle
{
    public static string SolveString(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        long result = Solve(path);
        return result.ToString(CultureInfo.InvariantCulture);
    }

    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(string[] rows)
    {
        var literalDescriptions = rows.TakeWhile(it => !string.IsNullOrEmpty(it)).ToList();
        var literalWires = literalDescriptions.Select(LiteralWire.Parse).ToArray();
        string[] binaryDescriptions = rows.Skip(literalDescriptions.Count + 1).ToArray();
        var binaryWires = binaryDescriptions.Select(BinaryWire.Parse).ToArray();
        var wireById = ((IEnumerable<Wire>)literalWires).Concat(binaryWires).ToFrozenDictionary(it => it.Id);
        WireEvaluator wireEvaluator = new(wireById);
        var zs = binaryWires.Where(it => it.Id.StartsWith('z')).ToFrozenDictionary(
            it => it.Id, it => wireEvaluator.Evaluate(it.Id));
        var zsOrdered = zs.OrderBy(it => it.Key).ToArray();
        long result = 0;
        for (int i = 0; i < zsOrdered.Length; ++i)
        {
            long value = zsOrdered[i].Value;
            Debug.Assert(value is 0 or 1);
            result |= value << i;
        }

        return result;
    }
}
