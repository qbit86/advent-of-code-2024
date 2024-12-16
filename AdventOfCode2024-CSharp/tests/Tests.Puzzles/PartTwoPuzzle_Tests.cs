namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 45L)]
    [InlineData("sample-2.txt", 64L)]
    [InlineData("input.txt", 679L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartTwoPuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
