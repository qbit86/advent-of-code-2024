namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 9L)]
    [InlineData("sample-2.txt", 0L)]
    [InlineData("input.txt", 1890L)]
    internal void Solve(string inputPath, long expected)
    {
        long actual = PartTwoPuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
