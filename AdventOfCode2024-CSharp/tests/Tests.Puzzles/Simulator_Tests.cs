using System.Linq;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

public sealed class Simulator_Tests
{
    [Fact]
    internal void Simulate()
    {
        string[] rows = ["p=2,4 v=2,-3"];
        var robotStates = rows.Select(RobotState.Parse).ToArray();
        var velocities = robotStates.Select(it => it.Velocity).ToArray();
        Simulator simulator = new(Vector128.Create(11L, 7L), velocities);
        var positions = robotStates.Select(it => it.Position).ToArray();
        simulator.Simulate(positions, positions);
        Assert.Equal(positions[0], Vector128.Create(4L, 1L));
        simulator.Simulate(positions, positions);
        Assert.Equal(positions[0], Vector128.Create(6L, 5L));
        simulator.Simulate(positions, positions);
        Assert.Equal(positions[0], Vector128.Create(8L, 2L));
        simulator.Simulate(positions, positions);
        Assert.Equal(positions[0], Vector128.Create(10L, 6L));
        simulator.Simulate(positions, positions);
        Assert.Equal(positions[0], Vector128.Create(1L, 3L));
    }
}
