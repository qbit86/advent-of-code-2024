namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 4L)]
    [InlineData("input.txt", 531L)]
    internal void Solve(string inputPath, long expected)
    {
        long actual = PartTwoPuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(new[] { 1, 3, 2, 3, 4 }, true)]
    [InlineData(new[] { -10, 1, 2, 3, 4 }, true)]
    [InlineData(new[] { 10, 1, 2, 3, 4 }, true)]
    [InlineData(new[] { 1, 2, 3, 4, -10 }, true)]
    [InlineData(new[] { 1, 2, 3, 4, 10 }, true)]
    internal void SolveSingle(int[] levels, bool expected)
    {
        bool actual = PartTwoPuzzle.IsReportWeaklySafe(levels);
        Assert.Equal(expected, actual);
    }
}
