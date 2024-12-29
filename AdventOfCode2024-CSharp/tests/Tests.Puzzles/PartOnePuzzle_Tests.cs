namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 37327623L)]
    [InlineData("input.txt", 17577894908L)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartOnePuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1, 8685429L)]
    [InlineData(10, 4700978L)]
    [InlineData(100, 15273692L)]
    [InlineData(2024, 8667524L)]
    internal void Solve2000_ShouldBeEqual(int seed, long expected)
    {
        long actual = PartOnePuzzle.Solve2000(seed);
        Assert.Equal(expected, actual);
    }
}
