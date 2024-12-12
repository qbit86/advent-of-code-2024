namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 80L)]
    [InlineData("sample-3.txt", 1206L)]
    [InlineData("sample-4.txt", 236L)]
    [InlineData("sample-5.txt", 368L)]
    [InlineData("input.txt", 818286L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartTwoPuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
