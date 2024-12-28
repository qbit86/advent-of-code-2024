namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 6, 12, 22L)]
    [InlineData("input.txt", 70, 1024, 416L)]
    internal void Solve_ShouldBeEqual(string inputPath, int upperBoundInclusive, int byteCount, long expected)
    {
        long actual = PartOnePuzzle.SolveLong(inputPath, upperBoundInclusive, byteCount);
        Assert.Equal(expected, actual);
    }
}
