using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Intrinsics;
using static AdventOfCode2024.TryHelpers;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal sealed class StatelessSimulatorPartTwo
{
    private static readonly V s_shift = Vector128.Create(0, 1, 0, 0);
    private readonly GridBase _grid;

    internal StatelessSimulatorPartTwo(GridBase grid) => _grid = grid;

    internal bool TryProcessInstruction(MutableState state, char instruction, out MutableState nextState)
    {
        var direction = Helpers.DirectionByInstruction[instruction];
        var nextRobotPositionCandidate = state.RobotPosition + direction;
        if (!_grid.IsWalkable(nextRobotPositionCandidate))
            return None(state, out nextState);

        if (!TryGetBox(state, nextRobotPositionCandidate, out int rootBoxId))
            return Some(state with { RobotPosition = nextRobotPositionCandidate }, out nextState);

        HashSet<int> explored = new(state.BoxPositionById.Count) { rootBoxId };
        Stack<int> frontier = new(state.BoxPositionById.Count);
        frontier.Push(rootBoxId);
        while (frontier.TryPop(out int boxId))
        {
            Debug.Assert(explored.Contains(boxId));
            var newBoxPosition = state.BoxPositionById[boxId] + direction;
            if (!_grid.IsWalkable(newBoxPosition) || !_grid.IsWalkable(newBoxPosition + s_shift))
                return None(state, out nextState);

            if (TryGetBox(state, newBoxPosition, out int leftNeighborBoxId) &&
                explored.Add(leftNeighborBoxId))
                frontier.Push(leftNeighborBoxId);

            if (TryGetBox(state, newBoxPosition + s_shift, out int rightNeighborBoxId) &&
                explored.Add(rightNeighborBoxId))
                frontier.Push(rightNeighborBoxId);
        }

        foreach (int boxId in explored)
        {
            var oldBoxPosition = state.BoxPositionById[boxId];
            bool removed = state.BoxIdByPosition.Remove(oldBoxPosition);
            Debug.Assert(removed);
        }

        foreach (int boxId in explored)
        {
            var oldBoxPosition = state.BoxPositionById[boxId];
            var newBoxPosition = oldBoxPosition + direction;
            state.BoxIdByPosition.Add(newBoxPosition, boxId);
            state.BoxPositionById[boxId] = newBoxPosition;
        }

        return Some(state with { RobotPosition = nextRobotPositionCandidate }, out nextState);
    }

    private static bool TryGetBox(MutableState state, V position, out int boxId)
    {
        if (state.BoxIdByPosition.TryGetValue(position, out boxId))
            return true;

        if (state.BoxIdByPosition.TryGetValue(position - s_shift, out boxId))
            return true;

        return false;
    }
}
