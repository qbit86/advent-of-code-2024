using System;
using System.Collections.Generic;

namespace AdventOfCode2024;

internal readonly struct WireEvaluator
{
    private readonly IReadOnlyDictionary<string, Wire> _wireById;
    private readonly Dictionary<string, int> _valueById;

    public WireEvaluator(IReadOnlyDictionary<string, Wire> wireById) : this(wireById, []) { }

    private WireEvaluator(IReadOnlyDictionary<string, Wire> wireById, Dictionary<string, int> valueById)
    {
        _valueById = valueById;
        _wireById = wireById;
    }

    internal int Evaluate(string wireId)
    {
        if (_valueById.TryGetValue(wireId, out int result))
            return result;

        result = EvaluateUnchecked(wireId);
        _ = _valueById.TryAdd(wireId, result);
        return result;
    }

    private int EvaluateUnchecked(string wireId) => EvaluateUnchecked(_wireById[wireId]);

    private int EvaluateUnchecked(Wire wire)
    {
        return wire switch
        {
            LiteralWire l => l.Value,
            BinaryWire b => b.Evaluate(Evaluate(b.Left), Evaluate(b.Right)),
            _ => throw new ArgumentOutOfRangeException(nameof(wire), wire, null)
        };
    }
}
