namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 0b100L)]
    [InlineData("sample-2.txt", 0b0011111101000L)]
    [InlineData("input.txt", 49520947122770L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartOnePuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
