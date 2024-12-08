namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", long.MinValue)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartOnePuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
