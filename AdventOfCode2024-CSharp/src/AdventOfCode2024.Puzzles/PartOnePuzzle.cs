using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace AdventOfCode2024;

public static class PartOnePuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(string[] rows)
    {
        string[] map = rows.TakeWhile(it => !string.IsNullOrWhiteSpace(it)).ToArray();
        string[] movesRows = rows.Skip(map.Length + 1).ToArray();
        string moves = string.Join(string.Empty, movesRows);
        return Solve(map, moves);
    }

    private static long Solve(string[] map, string moves)
    {
        GridPartOne grid = new(map);
        var robotPosition = grid.Where(it => it.Value is '@').Select(it => it.Key).Single();
        var boxPositions = grid.Where(it => it.Value is 'O').Select(it => it.Key).ToImmutableHashSet();
        ImmutableState state = new(robotPosition, boxPositions);
        StatelessSimulatorPartOne simulator = new(grid);
        foreach (char instruction in moves)
        {
            if (simulator.TryProcessInstruction(state, instruction, out var nextState))
                state = nextState;
        }

        return state.BoxPositions.Sum(Helpers.ComputeGpsCoordinate);
    }
}
