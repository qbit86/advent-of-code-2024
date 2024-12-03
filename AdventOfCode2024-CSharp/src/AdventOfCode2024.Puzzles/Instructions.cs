using System.Text.RegularExpressions;

namespace AdventOfCode2024;

internal interface IInstruction;

internal sealed record DoInstruction : IInstruction
{
    internal static DoInstruction Instance { get; } = new();
}

internal sealed record DontInstruction : IInstruction
{
    internal static DontInstruction Instance { get; } = new();
}

internal sealed record MulInstruction(Group Left, Group Right) : IInstruction;
