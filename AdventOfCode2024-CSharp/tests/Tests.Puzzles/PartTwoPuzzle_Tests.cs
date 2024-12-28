namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 6, 76, 1L)]
    [InlineData("sample.txt", 20, 76, 3L)]
    [InlineData("input.txt", 20, 100, 1011325L)]
    internal void Solve_ShouldBeEqual(
        string inputPath, int cheatPicosecondsUpperBoundInclusive, int savedPicosecondsLowerBound, long expected)
    {
        long actual = PartTwoPuzzle.Solve(inputPath, cheatPicosecondsUpperBoundInclusive, savedPicosecondsLowerBound);
        Assert.Equal(expected, actual);
    }
}
