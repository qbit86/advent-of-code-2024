namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 140L)]
    [InlineData("sample-2.txt", 772L)]
    [InlineData("sample-3.txt", 1930L)]
    [InlineData("input.txt", 1370100L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartOnePuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
