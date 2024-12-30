namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 7L)]
    [InlineData("input.txt", 998L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartOnePuzzle.SolveLong(inputPath);
        Assert.Equal(expected, actual);
    }
}
