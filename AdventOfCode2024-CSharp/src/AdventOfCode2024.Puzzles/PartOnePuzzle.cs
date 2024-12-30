using System;
using System.IO;
using System.Linq;
using System.Numerics.Tensors;

namespace AdventOfCode2024;

public static class PartOnePuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(string[] rows)
    {
        var separatorIndices = rows.Index()
            .Where(it => string.IsNullOrEmpty(it.Item))
            .Select(it => it.Index).Prepend(-1).Append(rows.Length).ToList();
        var schematicRanges = separatorIndices.Zip(
            separatorIndices.Skip(1), (leftExclusive, right) => new Range(leftExclusive + 1, right)).ToList();
        var schematicSegments = schematicRanges
            .Select(it => new ArraySegment<string>(rows, it.Start.Value, it.End.Value - it.Start.Value))
            .ToList();
        var lookup = schematicSegments.ToLookup(it => it[0][0] is '#');
        int[] sum = GC.AllocateUninitializedArray<int>(rows.Max(row => row.Length));
        int result = 0;
        foreach (var lockLines in lookup[true])
        {
            int[] lockHeights = GetHeights(lockLines);
            foreach (var keyLines in lookup[false])
            {
                int[] keyHeights = GetHeights(keyLines);
                TensorPrimitives.Add(lockHeights, keyHeights, sum.AsSpan());
                int heightBound = Math.Min(lockLines.Count, keyLines.Count) - 2;
                result += Convert.ToInt32(sum.All(it => heightBound >= it));
            }
        }

        return result;
    }

    private static int[] GetHeights(ArraySegment<string> lines)
    {
        int width = lines.Max(it => it.Length);
        return Enumerable.Range(0, width).Select(columnIndex => lines.Count(it => it[columnIndex] == '#') - 1)
            .ToArray();
    }
}
