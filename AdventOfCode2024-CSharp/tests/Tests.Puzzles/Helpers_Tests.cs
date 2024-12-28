using System;
using System.IO;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<int>;

public sealed class Helpers_Tests
{
    public static TheoryData<Grid, int, V, V, int> ComputeCheatingDistanceTheoryData { get; } =
        CreateComputeCheatingDistanceTheoryData();

    [Theory]
    [MemberData(nameof(ComputeCheatingDistanceTheoryData))]
    internal void ComputeCheatingDistance_ShouldBeEqual(
        Grid grid, int maxCheatPicoseconds, V start, V cheatingStart, int expected)
    {
        long actual = Helpers.ComputeCheatingSaving(grid, maxCheatPicoseconds, start, cheatingStart);
        Assert.Equal(expected, actual);
    }

    private static TheoryData<Grid, int, V, V, int> CreateComputeCheatingDistanceTheoryData()
    {
        const string inputPath = "sample.txt";
        string[] rows = File.ReadAllLines(inputPath);
        Grid grid = new(rows);

        bool hasStart = grid.TryFindKey('S', out var start);
        if (!hasStart)
            throw new ArgumentException(nameof(hasStart), nameof(inputPath));

        return new()
        {
            { grid, 2, start, Vector128.Create(1, 7, 0, 0), 12 },
            { grid, 2, start, Vector128.Create(7, 8, 0, 0), 38 },
            { grid, 2, start, Vector128.Create(7, 7, 0, 0), 64 }
        };
    }
}
