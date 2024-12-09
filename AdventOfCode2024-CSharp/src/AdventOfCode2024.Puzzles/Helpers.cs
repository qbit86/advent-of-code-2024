using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2024;

using static TryHelpers;

internal static class Helpers
{
    internal static List<BlockSpan> GetBlockSpans(string diskMap)
    {
        List<BlockSpan> blockSpans = new(diskMap.Length);
        int sourceStart = 0;
        for (int i = 0; i < diskMap.Length; ++i)
        {
            char c = diskMap[i];
            int length = c - '0';
            int rawId = i >> 1;
            bool isFile = int.IsEvenInteger(i);
            int id = isFile ? rawId : ~rawId;
            BlockSpan blockSpan = new(id, sourceStart, length);
            blockSpans.Add(blockSpan);
            sourceStart += length;
        }

        return blockSpans;
    }

    internal static long ComputeChecksum(List<BlockSpan> fileSpans) => fileSpans.Sum(ComputeBlockChecksum);

    private static long ComputeBlockChecksum(BlockSpan fileSpan)
    {
        long endInclusive = fileSpan.Start + fileSpan.Length - 1;
        long sum = (fileSpan.Start + endInclusive) * fileSpan.Length / 2;
        return fileSpan.Id * sum;
    }

    internal static bool TryTakeFileSpan(this Deque<BlockSpan> deque, out BlockSpan fileSpan)
    {
        while (true)
        {
            if (!deque.TryTake(out fileSpan))
                return false;
            if (fileSpan.IsFile)
                return true;
        }
    }

    internal static bool TryGetLast<T>(this List<T> items, [MaybeNullWhen(false)] out T item)
    {
        if (items.Count is 0)
            return None(out item);
        item = items[^1];
        return true;
    }
}
