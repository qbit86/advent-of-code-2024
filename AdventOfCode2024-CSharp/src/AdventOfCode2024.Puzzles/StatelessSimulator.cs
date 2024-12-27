using System;
using System.Collections.Generic;

namespace AdventOfCode2024;

using static TryHelpers;

public sealed class StatelessSimulator
{
    private readonly IReadOnlyList<int> _program;

    public StatelessSimulator(IReadOnlyList<int> program)
    {
        ArgumentNullException.ThrowIfNull(program);
        _program = program;
    }

    internal bool TrySimulateSingleInstruction(State state, out State nextState)
    {
        if (state.IP >= _program.Count)
            return None(state, out nextState);
        int opcode = _program[state.IP];
        int operand = _program[state.IP + 1];

        if (opcode is 1)
        {
            long result = state.B ^ operand;
            nextState = state with { B = result, IP = state.IP + 2 };
            return true;
        }

        if (opcode is 3)
        {
            if (state.A is 0)
                return Some(state with { IP = state.IP + 2 }, out nextState);
            nextState = state with { IP = operand };
            return true;
        }

        if (opcode is 4)
        {
            long result = state.B ^ state.C;
            nextState = state with { B = result, IP = state.IP + 2 };
            return true;
        }

        long comboOperandValue = operand switch
        {
            >= 0 and < 4 => operand,
            4 => state.A,
            5 => state.B,
            6 => state.C,
            _ => throw new InvalidOperationException($"Unexpected operand: {operand}")
        };

        if (opcode is 0)
        {
            long result = state.A >> (int)comboOperandValue;
            nextState = state with { A = result, IP = state.IP + 2 };
            return true;
        }

        if (opcode is 2)
        {
            long result = comboOperandValue & 0b111;
            nextState = state with { B = result, IP = state.IP + 2 };
            return true;
        }

        if (opcode is 5)
        {
            long result = comboOperandValue & 0b111;
            var nextOutput = state.Output.Add(result);
            nextState = state with { Output = nextOutput, IP = state.IP + 2 };
            return true;
        }

        if (opcode is 6)
        {
            long result = state.A >> (int)comboOperandValue;
            nextState = state with { B = result, IP = state.IP + 2 };
            return true;
        }

        if (opcode is 7)
        {
            long result = state.A >> (int)comboOperandValue;
            nextState = state with { C = result, IP = state.IP + 2 };
            return true;
        }

        throw new InvalidOperationException($"Unexpected opcode: {opcode}");
    }

    internal bool TrySimulateSingleOutput(State state, out State nextState)
    {
        while (TrySimulateSingleInstruction(state, out nextState))
        {
            if (nextState.Output.Count > state.Output.Count)
                return true;
            state = nextState;
        }

        return false;
    }

    public State SimulateAllInstructions(State state)
    {
        while (TrySimulateSingleInstruction(state, out var nextState))
            state = nextState;

        return state;
    }

    public State SimulateOutputs(State state, int outputCount)
    {
        while (TrySimulateSingleInstruction(state, out var nextState))
        {
            state = nextState;
            if (nextState.Output.Count >= outputCount)
                break;
        }

        return state;
    }
}
