#if false
using System.Globalization;

namespace AdventOfCode2024;

internal interface INode;

internal sealed record VariableNode : INode
{
    internal static VariableNode Instance { get; } = new();

    public override string ToString() => "?";
}

internal sealed record LiteralNode(int Value) : INode
{
    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
}

internal abstract record UnaryNode(INode Value) : INode;

internal abstract record BinaryNode(INode Left, INode Right) : INode;

internal sealed record ShiftNode(INode Left, INode Right) : BinaryNode(Left, Right)
{
    internal static INode Create(INode left, INode right) =>
        left is LiteralNode l && right is LiteralNode r
            ? new LiteralNode(l.Value >> r.Value)
            : new ShiftNode(left, right);
}

internal sealed record ModNode(INode Value) : UnaryNode(Value)
{
    internal static INode Create(INode value) =>
        value is LiteralNode v ? new LiteralNode(v.Value & 0b111) : new ModNode(value);
}

internal sealed record XorNode(INode Left, INode Right) : BinaryNode(Left, Right)
{
    internal static INode Create(INode left, INode right) =>
        left is LiteralNode l && right is LiteralNode r
            ? new LiteralNode(l.Value ^ r.Value)
            : new XorNode(left, right);
}
#endif
