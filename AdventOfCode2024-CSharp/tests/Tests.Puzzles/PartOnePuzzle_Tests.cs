namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 480L)]
    [InlineData("sample-2.txt", 280L)]
    [InlineData("sample-3.txt", 0L)]
    [InlineData("sample-4.txt", 200L)]
    [InlineData("sample-5.txt", 0L)]
    [InlineData("input.txt", 30413L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartOnePuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
