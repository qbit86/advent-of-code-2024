using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AdventOfCode2024;

public static class PartTwoPuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string line = File.ReadAllText(path).TrimEnd();
        return Solve(line, 75);
    }

    private static long Solve(string line, int blinkCount)
    {
        long[] stones = line.Split(' ').Select(it => long.Parse(it, CultureInfo.InvariantCulture)).ToArray();
        return Solve(stones, blinkCount);
    }

    public static long Solve(long[] stones, int blinkCount)
    {
        ArgumentNullException.ThrowIfNull(stones);
        if (blinkCount < 1)
            return stones.LongLength;

        var stoneCounts = stones.Select(it => Compute([], it, blinkCount));
        return stoneCounts.Sum();
    }

    private static long GetFromCacheOrCompute(Dictionary<Node, long> cache, long stone, int remainingBlinks)
    {
        Node node = new(stone, remainingBlinks);
        if (cache.TryGetValue(node, out long cachedResult))
            return cachedResult;
        long result = Compute(cache, stone, remainingBlinks);
        cache.Add(node, result);
        return result;
    }

    private static long Compute(Dictionary<Node, long> cache, long stone, int remainingBlinks)
    {
        if (remainingBlinks is 0)
            return 1;

        if (stone is 0)
            return GetFromCacheOrCompute(cache, 1, remainingBlinks - 1);

        int digitCount = Helpers.ComputeDigitCount(stone);
        if ((digitCount & 1) is 0)
        {
            int newDigitCount = digitCount >> 1;
            long divisor = (long)Math.Pow(10, newDigitCount);
            (long quotient, long remainder) = Math.DivRem(stone, divisor);
            return GetFromCacheOrCompute(cache, quotient, remainingBlinks - 1) +
                GetFromCacheOrCompute(cache, remainder, remainingBlinks - 1);
        }

        return GetFromCacheOrCompute(cache, checked(stone * 2024), remainingBlinks - 1);
    }
}
