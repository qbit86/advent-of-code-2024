namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 161L)]
    [InlineData("input.txt", 183669043L)]
    internal void Solve(string inputPath, long expected)
    {
        long actual = PartOnePuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
