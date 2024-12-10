namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 1L)]
    [InlineData("sample-2.txt", 36L)]
    [InlineData("sample-3.txt", 1L)]
    [InlineData("sample-5.txt", 2L)]
    [InlineData("input.txt", 459L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartOnePuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
