using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AdventOfCode2024;

public static class PartOnePuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string line = File.ReadAllText(path).TrimEnd();
        return Solve(line, 25);
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

        var oldStones = stones.ToList();
        List<long> newStones = [];
        for (int i = 0; i < blinkCount; ++i)
        {
            newStones.Clear();
            foreach (long stone in oldStones)
                ProcessStone(stone, newStones);
            (oldStones, newStones) = (newStones, oldStones);
        }

        return oldStones.Count;
    }

    private static void ProcessStone(long stone, List<long> newStones)
    {
        if (stone is 0)
        {
            newStones.Add(1);
            return;
        }

        int digitCount = Helpers.ComputeDigitCount(stone);
        if ((digitCount & 1) is 0)
        {
            int newDigitCount = digitCount >> 1;
            long divisor = (long)Math.Pow(10, newDigitCount);
            (long quotient, long remainder) = Math.DivRem(stone, divisor);
            newStones.Add(quotient);
            newStones.Add(remainder);
            return;
        }

        newStones.Add(stone * 2024);
    }
}
