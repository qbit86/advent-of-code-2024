using System;
using System.IO;

namespace AdventOfCode2024;

public sealed class DistanceCalculator_Tests
{
    [Theory]
    [InlineData("sample.txt", 84)]
    internal void GetSignedDistance_ShouldBeEqual(string inputPath, int expected)
    {
        string[] rows = File.ReadAllLines(inputPath);
        Grid grid = new(rows);

        bool hasStart = grid.TryFindKey('S', out var start);
        if (!hasStart)
            throw new ArgumentException(nameof(hasStart), nameof(inputPath));

        var distanceMap = Helpers.CreateDistanceMap(grid, start);
        var distanceCalculator = DistanceCalculator.Create(distanceMap);

        bool hasEnd = grid.TryFindKey('E', out var end);
        if (!hasEnd)
            throw new ArgumentException(nameof(hasEnd), nameof(inputPath));

        int actual = distanceCalculator.GetSignedDistance(start, end);
        Assert.Equal(expected, actual);
    }
}
