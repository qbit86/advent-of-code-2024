namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 10092L)]
    [InlineData("sample-2.txt", 2028L)]
    [InlineData("sample-3.txt", 101L)]
    [InlineData("input.txt", 1456590L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartOnePuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
