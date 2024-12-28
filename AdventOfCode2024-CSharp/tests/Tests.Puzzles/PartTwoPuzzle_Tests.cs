using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<int>;

public sealed class PartTwoPuzzle_Tests
{
    public static TheoryData<string, int, int, V> ShouldBeEqualTheoryData { get; } = CreateTheoryData();

    [Theory]
    [MemberData(nameof(ShouldBeEqualTheoryData), MemberType = typeof(PartTwoPuzzle_Tests))]
    internal void Solve_ShouldBeEqual(string inputPath, int upperBoundInclusive, int byteCountLowerBound, V expected)
    {
        var actual = PartTwoPuzzle.SolveVector(inputPath, upperBoundInclusive, byteCountLowerBound);
        Assert.Equal(expected, actual);
    }

    private static TheoryData<string, int, int, V> CreateTheoryData() => new()
    {
        { "sample.txt", 6, 12, Vector128.Create(6, 1, 0, 0) },
        { "input.txt", 70, 1024, Vector128.Create(50, 23, 0, 0) }
    };
}
