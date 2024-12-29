namespace AdventOfCode2024;

public sealed class Solver_Tests
{
    [Theory]
    [InlineData("029A", 68L)]
    [InlineData("980A", 60L)]
    [InlineData("179A", 68L)]
    [InlineData("456A", 64L)]
    [InlineData("379A", 64L)]
    internal void Solve_WhenDepthIs2_ShouldBeEqual(string code, long expected)
    {
        Solver solver = new(2);
        long actual = solver.Solve(code);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("029A", 2, 68L)]
    [InlineData("029A", 1, 28L)]
    [InlineData("029A", 0, 12L)]
    internal void Solve_ShouldBeEqual(string code, int depth, long expected)
    {
        Solver solver = new(depth);
        long actual = solver.Solve(code);
        Assert.Equal(expected, actual);
    }
}
