namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("input.txt", 228449040027793L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartTwoPuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(new long[] { 0, 1, 10, 99, 999 }, 1, 7L)]
    [InlineData(new long[] { 125, 17 }, 6, 22L)]
    [InlineData(new long[] { 125, 17 }, 25, 55312L)]
    [InlineData(new long[] { 475449, 2599064, 213, 0, 2, 65, 5755, 51149 }, 25, 193269L)]
    internal void Solve_WhenGivenBlinkCount_ShouldBeEqual(long[] stones, int blinkCount, long expected)
    {
        long actual = PartTwoPuzzle.Solve(stones, blinkCount);
        Assert.Equal(expected, actual);
    }
}
