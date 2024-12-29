using System.Linq;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("sample-2.txt", 23L)]
    [InlineData("input.txt", 1931)]
    internal void Solve_ShouldBeEqual(string inputPath, long expected)
    {
        long actual = PartTwoPuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }

    [Fact]
    internal void EnumeratePrices_ShouldBeEqual()
    {
        int[] expected = [3, 0, 6, 5, 4, 4, 6, 4, 4, 2];
        int[] actual = PartTwoPuzzle.EnumeratePrices(123).Take(10).ToArray();
        Assert.Equal(expected, actual);
    }

    [Fact]
    internal void CreateWindows_ShouldBeEqual()
    {
        Window[] expected =
        [
            new(4, 4, Vector128.Create(-3, 6, -1, -1)),
            new(5, 4, Vector128.Create(6, -1, -1, 0)),
            new(6, 6, Vector128.Create(-1, -1, 0, 2)),
            new(7, 4, Vector128.Create(-1, 0, 2, -2)),
            new(8, 4, Vector128.Create(0, 2, -2, 0)),
            new(9, 2, Vector128.Create(2, -2, 0, -2))
        ];
        var actual = PartTwoPuzzle.CreateWindows(123).Take(expected.Length).ToArray();
        Assert.Equal(expected, actual);
    }
}
