using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2024;

public static class PartTwoPuzzle
{
    public static string SolveString(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] result = Solve(path);
        return string.Join(',', result);
    }

    private static string[] Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static string[] Solve(string[] rows)
    {
        var literalDescriptions = rows.TakeWhile(it => !string.IsNullOrEmpty(it)).ToList();
        var literalWires = literalDescriptions.Select(LiteralWire.Parse).ToArray();
        var binaryDescriptions = rows.Skip(literalDescriptions.Count + 1);
        var binaryWires = binaryDescriptions.Select(BinaryWire.Parse).ToArray();
        var wireById = ((IEnumerable<Wire>)literalWires).Concat(binaryWires).ToFrozenDictionary(it => it.Id);

        HashSet<Wire> swappedWires = [];

        var zs = binaryWires.Where(it => it.Id.StartsWith('z')).ToList();
        var maxZ = zs.MaxBy(it => it.Id);
        var ordinaryZs = zs.Where(it => it != maxZ).ToList();
        var swappedZs = ordinaryZs.Where(IsNotXor).ToList();
        swappedWires.UnionWith(swappedZs);

        var ors = binaryWires.OfType<OrWire>();
        foreach (var wire in ors)
        {
            var leftInput = wireById[wire.Left];
            if (leftInput is not AndWire)
                swappedWires.Add(leftInput);

            var rightInput = wireById[wire.Right];
            if (rightInput is not AndWire)
                swappedWires.Add(rightInput);
        }

        var xors = binaryWires.OfType<XorWire>();
        var swappedXors = xors.Where(IsNotConnectedToXyz);
        swappedWires.UnionWith(swappedXors);

        var zInputIds = ordinaryZs.Except(swappedZs)
            .Where(it => it.Id is not "z01")
            .SelectMany<BinaryWire, string>(it => [it.Left, it.Right]).ToList();
        var swappedZInputs = zInputIds.Select(it => wireById[it]).OfType<AndWire>().ToList();
        swappedWires.UnionWith(swappedZInputs);

#if false
        using TextWriter writer =
            File.CreateText(Path.Join(Helpers.EnsureOutputDirectory(), $"wires-{DateTime.Now:HH-mm-ss}.gv"));
#else
        var writer = TextWriter.Null;
#endif
        DumpWires(literalWires, binaryWires, swappedWires, writer);
        writer.Close();

        string[] result = swappedWires.Select(it => it.Id).ToArray();
        Array.Sort(result);
        return result;
    }

    private static bool IsNotXor(BinaryWire wire) => wire is not XorWire;

    private static bool IsNotConnectedToXyz(XorWire wire)
    {
        if (wire.Id.StartsWith('z'))
            return false;
        if (wire.Left.StartsWith('x') && wire.Right.StartsWith('y'))
            return false;
        if (wire.Left.StartsWith('y') && wire.Right.StartsWith('x'))
            return false;
        return true;
    }

    private static void DumpWires<TSet>(
        IReadOnlyList<LiteralWire> literalWires,
        IReadOnlyList<BinaryWire> binaryWires,
        TSet swappedWires,
        TextWriter writer)
        where TSet : IReadOnlySet<Wire>
    {
        if (writer == TextWriter.Null)
            return;

        writer.WriteLine("digraph {");

        writer.WriteLine("    node [fontname=\"Helvetica\"]");

        writer.WriteLine();
        foreach (var wire in literalWires)
            writer.WriteLine($"    {wire.Id}");

        writer.WriteLine();
        foreach (var wire in binaryWires)
        {
            string label = GetLabel(wire);
            string nodeId = $"{label}_{wire.Left}_{wire.Right}";
            string gateFillColor = wire is OrWire ? "PowderBlue" : "transparent";
            writer.WriteLine(
                $"    {nodeId}\t[fillcolor={gateFillColor} label={label} shape=component style=filled]");

            string outputFillColor = GetOutputFillColor(wire);
            writer.WriteLine($"    {wire.Id}\t[fillcolor={outputFillColor} style=filled]");
            writer.WriteLine($"    {wire.Left} -> {nodeId}");
            writer.WriteLine($"    {wire.Right} -> {nodeId}");
            writer.WriteLine($"    {nodeId} -> {wire.Id}\t[dir=none]");
        }

        writer.WriteLine('}');
        writer.Flush();
        return;

        string GetOutputFillColor(Wire wire)
        {
            if (swappedWires.Contains(wire))
                return "Orange";
            if (wire.Id.StartsWith('z'))
                return "Gray";
            return "transparent";
        }
    }

    private static string GetLabel(BinaryWire wire) => wire switch
    {
        AndWire => "AND",
        OrWire => "OR",
        XorWire => "XOR",
        _ => throw new ArgumentOutOfRangeException(nameof(wire), wire, null)
    };
}
