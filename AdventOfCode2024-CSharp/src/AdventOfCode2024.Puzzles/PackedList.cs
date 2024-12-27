using System;
using System.Collections.Generic;

namespace AdventOfCode2024;

public readonly partial struct PackedList : IReadOnlyList<long>, IEquatable<PackedList>
{
    public const int BitsPerItem = 3;

    private const int UpperBoundInclusive = (sizeof(long) << 3) / BitsPerItem;

    private PackedList(long data, int count)
    {
        Data = data;
        Count = count;
    }

    public static PackedList Empty { get; } = Create(0L, 0);

    public int Count { get; }

    public long Data { get; }

    public static PackedList Create(long data, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count, nameof(count));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(count, UpperBoundInclusive, nameof(count));
        return new(data, count);
    }

    public PackedList Add(long item)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(item);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(item, 1 << BitsPerItem);
        int newCount = Count + 1;
        if (newCount > UpperBoundInclusive)
            throw new InvalidOperationException($"Cannot contain more items than {UpperBoundInclusive}.");
        int shiftAmount = Count * BitsPerItem;
        long mask = (1L << shiftAmount) - 1L;
        long newData = (item << shiftAmount) | (Data & mask);
        return new(newData, newCount);
    }

    public long this[int index]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, UpperBoundInclusive);
            int shiftAmount = index * BitsPerItem;
            long shiftedData = Data >> shiftAmount;
            const long mask = (1L << BitsPerItem) - 1;
            return shiftedData & mask;
        }
    }
}
