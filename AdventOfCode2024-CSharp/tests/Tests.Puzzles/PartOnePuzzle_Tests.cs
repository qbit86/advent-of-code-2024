namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 18L)]
    [InlineData("sample-2.txt", 4L)]
    [InlineData("input.txt", 2493L)]
    internal void Solve(string inputPath, long expected)
    {
        long actual = PartOnePuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
