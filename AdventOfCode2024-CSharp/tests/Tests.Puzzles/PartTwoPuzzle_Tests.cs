#if false
namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", long.MinValue)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartTwoPuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
#endif
