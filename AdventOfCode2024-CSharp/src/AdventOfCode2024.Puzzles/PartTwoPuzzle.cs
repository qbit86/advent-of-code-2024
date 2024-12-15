using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<long>;

public static class PartTwoPuzzle
{
    private static readonly V[] s_directions =
        [Vector128.Create(0, 1), Vector128.Create(1, 0), Vector128.Create(0, -1), Vector128.Create(-1, 0)];

    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(string[] rows)
    {
        var robotStates = rows.Select(RobotState.Parse).ToArray();
        var velocities = robotStates.Select(it => it.Velocity).ToArray();
        Simulator simulator = new(Helpers.LongSize, velocities);
        var positions = robotStates.Select(it => it.Position).ToArray();

        var infos = EnumerateInfos(simulator, positions);
        var indexedInfos = infos.Index().Select(it => new IndexedInfo(it.Index, it.Item.Positions, it.Item.Score));
        var indexedInfoArray = indexedInfos.Take(10000).ToArray();

        var min = indexedInfoArray.MinBy(it => it.Score);
        Dump(min);
        return min.Index;
    }

    private static IEnumerable<Info> EnumerateInfos(Simulator simulator, IReadOnlyList<V> initialPositions)
    {
        var positions = initialPositions.ToArray();
        while (true)
        {
            float score = ComputeScore(positions);
            yield return new([.. positions], score);
            simulator.Simulate(positions, positions);
        }
    }

    private static float ComputeScore(V[] positions)
    {
        Span<float> histogram = stackalloc float[5];
        histogram.Clear();
        var positionSet = positions.ToHashSet();
        foreach (var position in positionSet)
        {
            int brightness = ComputeBrightness(positionSet, position);
            histogram[brightness] += 1f;
        }

        return -histogram[2];
    }

    private static int ComputeBrightness(IReadOnlySet<V> positionSet, V position)
    {
        var neighborhood = s_directions.Select(it => position + it);
        return neighborhood.Count(positionSet.Contains);
    }

    private static void Dump(IndexedInfo info)
    {
        string prefix = info.Index.ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
        string basename = $"{prefix}-{info.Score:F3}.txt";
        string outputDirectory = Helpers.EnsureOutputDirectory();
        string path = Path.Combine(outputDirectory, basename);
        int columnCount = Helpers.Size[0];
        string emptyLine = new(' ', columnCount);
        var lookup = info.Positions.ToLookup(it => it[1]);
        char[] buffer = ArrayPool<char>.Shared.Rent(columnCount);
        var span = buffer.AsSpan(0, columnCount);
        using var stream = File.OpenWrite(path);
        using StreamWriter writer = new(stream);
        for (int rowIndex = 0; rowIndex < Helpers.Size[1]; ++rowIndex)
        {
            var g = lookup[rowIndex];
            bool hasNonEnumeratedCount = g.TryGetNonEnumeratedCount(out int count);
            Debug.Assert(hasNonEnumeratedCount);
            if (count is 0)
            {
                writer.WriteLine(emptyLine);
                continue;
            }

            span.Fill(' ');
            foreach (var position in g)
                span[(int)position[0]] = '#';
            writer.WriteLine(span);
        }

        stream.Flush(true);
        ArrayPool<char>.Shared.Return(buffer);
    }
}

internal readonly record struct Info(V[] Positions, float Score);

internal readonly record struct IndexedInfo(int Index, V[] Positions, float Score);

internal readonly struct ScoreComparer : IComparer<IndexedInfo>
{
    internal static IComparer<IndexedInfo> Instance { get; } = new ScoreComparer();

    public int Compare(IndexedInfo x, IndexedInfo y) => x.Score.CompareTo(y.Score);
}
