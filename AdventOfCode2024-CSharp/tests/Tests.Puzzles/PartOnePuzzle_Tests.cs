namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 7036L)]
    [InlineData("sample-2.txt", 11048L)]
    [InlineData("input.txt", 115500L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartOnePuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
