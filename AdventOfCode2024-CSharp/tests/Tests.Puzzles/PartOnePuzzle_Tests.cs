namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 55312L)]
    [InlineData("input.txt", 193269L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartOnePuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(new long[] { 0, 1, 10, 99, 999 }, 1, 7L)]
    [InlineData(new long[] { 125, 17 }, 6, 22L)]
    internal void Solve_WhenGivenBlinkCount_ShouldBeEqual(long[] stones, int blinkCount, long expected)
    {
        long actual = PartOnePuzzle.Solve(stones, blinkCount);
        Assert.Equal(expected, actual);
    }
}
