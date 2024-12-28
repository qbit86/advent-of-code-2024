using System.Runtime.Intrinsics;
using static AdventOfCode2024.TryHelpers;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal sealed class StatelessSimulatorPartOne
{
    private readonly GridBase _grid;

    internal StatelessSimulatorPartOne(GridBase grid) => _grid = grid;

    internal bool TryProcessInstruction(ImmutableState state, char instruction, out ImmutableState nextState)
    {
        var direction = Helpers.DirectionByInstruction[instruction];
        var nextRobotPositionCandidate = state.RobotPosition + direction;
        if (!_grid.IsWalkable(nextRobotPositionCandidate))
            return None(state, out nextState);

        if (!state.BoxPositions.Contains(nextRobotPositionCandidate))
            return Some(state with { RobotPosition = nextRobotPositionCandidate }, out nextState);

        V? nextBoxPosition = null;
        for (var nextBoxPositionCandidate = nextRobotPositionCandidate;; nextBoxPositionCandidate += direction)
        {
            if (!_grid.IsWalkable(nextBoxPositionCandidate))
                break;

            if (!state.BoxPositions.Contains(nextBoxPositionCandidate))
            {
                nextBoxPosition = nextBoxPositionCandidate;
                break;
            }
        }

        if (!nextBoxPosition.HasValue)
            return None(state, out nextState);

        var nextBoxPositions = state.BoxPositions.Remove(nextRobotPositionCandidate)
            .Add(nextBoxPosition.GetValueOrDefault());
        nextState = new(nextRobotPositionCandidate, nextBoxPositions);
        return true;
    }
}
