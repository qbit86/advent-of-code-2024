namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("sample-2.txt", 48L)]
    [InlineData("input.txt", 59097164L)]
    internal void Solve(string inputPath, long expected)
    {
        long actual = PartTwoPuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
