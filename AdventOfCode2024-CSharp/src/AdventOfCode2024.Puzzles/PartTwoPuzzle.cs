using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2024;

public static class PartTwoPuzzle
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
        GridPartTwo grid = new(map);
        var robotPosition = grid.Where(it => it.Value is '@').Select(it => it.Key).Single();
        var boxPositionById = grid.Where(it => it.Value is '[').Select(it => it.Key).ToArray();
        var boxIdByPosition = boxPositionById.Index().ToDictionary(it => it.Item, it => it.Index);
        MutableState state = new(robotPosition, boxPositionById, boxIdByPosition);
        StatelessSimulatorPartTwo simulator = new(grid);
        foreach (char instruction in moves)
        {
            if (simulator.TryProcessInstruction(state, instruction, out var nextState))
                state = nextState;
        }

        return state.BoxPositionById.Sum(Helpers.ComputeGpsCoordinate);
    }
}
