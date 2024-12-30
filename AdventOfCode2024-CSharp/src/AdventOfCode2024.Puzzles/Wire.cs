using System;
using System.Globalization;

namespace AdventOfCode2024;

internal abstract record Wire(string Id);

internal sealed record LiteralWire(string Id, int Value) : Wire(Id)
{
    internal static LiteralWire Parse(string line)
    {
        string[] parts = line.Split(": ");
        return new LiteralWire(parts[0], int.Parse(parts[1], CultureInfo.InvariantCulture));
    }
}

internal abstract record BinaryWire(string Id, string Left, string Right) : Wire(Id)
{
    private static readonly string[] s_separators = [" ", "->"];

    internal abstract int Evaluate(int left, int right);

    internal static BinaryWire Parse(string line)
    {
        string[] parts = line.Split(
            s_separators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        string left = parts[0];
        string operation = parts[1];
        string right = parts[2];
        string id = parts[3];
        return operation switch
        {
            "AND" => new AndWire(id, left, right),
            "OR" => new OrWire(id, left, right),
            "XOR" => new XorWire(id, left, right),
            _ => throw new FormatException($"'{operation}' is not a valid operation in '{line}'.")
        };
    }
}

internal sealed record AndWire(string Id, string Left, string Right) : BinaryWire(Id, Left, Right)
{
    internal override int Evaluate(int left, int right) => left & right;
}

internal sealed record OrWire(string Id, string Left, string Right) : BinaryWire(Id, Left, Right)
{
    internal override int Evaluate(int left, int right) => left | right;
}

internal sealed record XorWire(string Id, string Left, string Right) : BinaryWire(Id, Left, Right)
{
    internal override int Evaluate(int left, int right) => left ^ right;
}
