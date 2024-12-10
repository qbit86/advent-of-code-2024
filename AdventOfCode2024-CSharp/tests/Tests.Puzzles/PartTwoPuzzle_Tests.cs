namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("sample-2.txt", 81L)]
    [InlineData("sample-3.txt", 3L)]
    [InlineData("sample-4.txt", 13L)]
    [InlineData("sample-5.txt", 227L)]
    [InlineData("sample-6.txt", 72748L)]
    [InlineData("input.txt", 1034L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartTwoPuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
