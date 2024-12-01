using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2024;

public static class PartTwoPuzzle
{
    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path, Encoding.UTF8);
        return Solve(lines);
    }

    private static long Solve<TRows>(TRows rows)
        where TRows : IReadOnlyList<string>
    {
        (int[] leftNumbers, int[] rightNumbers) = Helpers.Parse(rows);

        var countByNumber = rightNumbers.CountBy(it => it).ToFrozenDictionary();
        return leftNumbers.Aggregate(0L, AggregateFunc);

        long AggregateFunc(long acc, int number) =>
            acc + Math.BigMul(number, countByNumber.GetValueOrDefault(number, 0));
    }
}
