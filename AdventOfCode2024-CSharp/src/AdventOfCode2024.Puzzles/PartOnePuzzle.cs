using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2024;

public static class PartOnePuzzle
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
            if (!deque.TryTakeFront(out var leftFreeSpan))
                break;

            if (leftFreeSpan.IsFile)
            {
                destinationFileSpans.Add(leftFreeSpan);
                continue;
            }

            Debug.Assert(leftFreeSpan.IsFree);
            if (!deque.TryTakeFileSpan(out var rightFileSpan))
                break;
            Debug.Assert(rightFileSpan.IsFile);

            int destinationStart = destinationFileSpans.TryGetLast(out var last) ? last.Start + last.Length : 0;
            if (leftFreeSpan.Length < rightFileSpan.Length)
            {
                destinationFileSpans.Add(rightFileSpan with { Start = destinationStart, Length = leftFreeSpan.Length });
                var remainingFileSpan = rightFileSpan with { Length = rightFileSpan.Length - leftFreeSpan.Length };
                _ = deque.TryAdd(remainingFileSpan);
                continue;
            }

            if (rightFileSpan.Length < leftFreeSpan.Length)
            {
                destinationFileSpans.Add(rightFileSpan with { Start = destinationStart });
                var remainingFreeSpan = leftFreeSpan with { Length = leftFreeSpan.Length - rightFileSpan.Length };
                _ = deque.TryAddFront(remainingFreeSpan);
                continue;
            }

            Debug.Assert(leftFreeSpan.Length == rightFileSpan.Length);
            destinationFileSpans.Add(rightFileSpan with { Start = destinationStart });
        }

        return Helpers.ComputeChecksum(destinationFileSpans);
    }
}
