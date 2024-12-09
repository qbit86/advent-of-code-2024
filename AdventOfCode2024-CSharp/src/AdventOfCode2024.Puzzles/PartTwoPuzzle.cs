using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2024;

public static class PartTwoPuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string text = File.ReadAllText(path);
        return SolveCore(text.TrimEnd());
    }

    private static long SolveCore(string diskMap)
    {
        var sourceBlockSpans = Helpers.GetBlockSpans(diskMap);

        List<BlockSpan> destinationFileSpans = [];
        var deque = Deque<BlockSpan>.Create(sourceBlockSpans);
        while (true)
        {
            if (!deque.TryTakeFront(out var leftBlockSpan))
                break;

            if (leftBlockSpan.IsFile)
            {
                destinationFileSpans.Add(leftBlockSpan);
                continue;
            }

            Debug.Assert(leftBlockSpan.IsFree);
            if (leftBlockSpan.Length > 0)
                _ = deque.TryAddFront(leftBlockSpan);

            if (!deque.TryTakeFileSpan(out var rightFileSpan))
                break;
            Debug.Assert(rightFileSpan.IsFile);

            int freeBlockSpanIndex = -1;
            for (int i = 0; i < deque.Count; ++i)
            {
                var blockSpan = deque[i];
                if (blockSpan.Id == rightFileSpan.Id)
                    break;
                if (blockSpan.IsFile)
                    continue;
                if (blockSpan.Length < rightFileSpan.Length)
                    continue;

                freeBlockSpanIndex = i;
                break;
            }

            if (freeBlockSpanIndex < 0)
            {
                destinationFileSpans.Add(rightFileSpan);
                continue;
            }

            var freeBlockSpan = deque[freeBlockSpanIndex];
            Debug.Assert(freeBlockSpan.IsFree);
            Debug.Assert(freeBlockSpan.Length >= rightFileSpan.Length);

            var newFileBlockSpan = rightFileSpan with { Start = freeBlockSpan.Start };
            var newFreeBlockSpan = freeBlockSpan with
            {
                Start = freeBlockSpan.Start + rightFileSpan.Length,
                Length = freeBlockSpan.Length - rightFileSpan.Length
            };

            destinationFileSpans.Add(newFileBlockSpan);
            deque[freeBlockSpanIndex] = newFreeBlockSpan;
        }

        while (deque.TryTakeFront(out var blockSpan))
        {
            if (blockSpan.IsFile)
                destinationFileSpans.Add(blockSpan);
        }

        return Helpers.ComputeChecksum(destinationFileSpans);
    }
}
