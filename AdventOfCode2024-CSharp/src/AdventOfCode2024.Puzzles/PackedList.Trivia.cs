using System;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode2024;

public readonly partial struct PackedList
{
    public IEnumerator<long> GetEnumerator()
    {
        for (int i = 0; i < Count; ++i)
            yield return this[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool Equals(PackedList other) => Count == other.Count && Data == other.Data;

    public override bool Equals(object? obj) => obj is PackedList other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Count, Data);

    public static bool operator ==(PackedList left, PackedList right) => left.Equals(right);

    public static bool operator !=(PackedList left, PackedList right) => !left.Equals(right);
}
