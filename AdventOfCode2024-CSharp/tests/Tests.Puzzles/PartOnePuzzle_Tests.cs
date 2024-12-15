using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", 11L, 7L, 12L)]
    [InlineData("sample-2.txt", 11L, 7L, 0L)]
    [InlineData("input.txt", 101L, 103L, 214400550L)]
    internal void Solve_ShouldBeEqual(string inputPath, long width, long height, long expected)
    {
        long actual = PartOnePuzzle.Solve(inputPath, Vector128.Create(width, height));
        Assert.Equal(expected, actual);
    }
}
