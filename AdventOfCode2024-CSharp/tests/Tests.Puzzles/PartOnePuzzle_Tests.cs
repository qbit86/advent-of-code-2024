namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 64, 1L)]
    [InlineData("sample.txt", 40, 2L)]
    [InlineData("sample.txt", 38, 3L)]
    [InlineData("sample.txt", 36, 4L)]
    [InlineData("sample.txt", 20, 5L)]
    [InlineData("sample.txt", 12, 8L)]
    [InlineData("sample.txt", 1, 44L)]
    [InlineData("input.txt", 100, 1490L)]
    internal void Solve_ShouldBeEqual(string inputPath, int savedPicosecondsLowerBound, long expected)
    {
        long actual = PartOnePuzzle.Solve(inputPath, savedPicosecondsLowerBound);
        Assert.Equal(expected, actual);
    }
}
