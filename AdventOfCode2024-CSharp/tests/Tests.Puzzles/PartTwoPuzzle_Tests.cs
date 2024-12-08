namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 34L)]
    [InlineData("input.txt", 1263L)]
    internal void Solve(string inputPath, long expected)
    {
        long actual = PartTwoPuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
