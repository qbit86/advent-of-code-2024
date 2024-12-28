using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using static TryHelpers;
using V = Vector128<int>;

public sealed class Grid : IEnumerable<KeyValuePair<V, char>>
{
    private readonly string[] _rows;

    public Grid(string[] rows)
    {
        ArgumentNullException.ThrowIfNull(rows);
        _rows = rows;
    }

    internal int RowCount => _rows.Length;

    public IEnumerator<KeyValuePair<V, char>> GetEnumerator()
    {
        for (int rowIndex = 0; rowIndex < _rows.Length; ++rowIndex)
        {
            for (int columnIndex = 0; columnIndex < _rows.Length; ++columnIndex)
                yield return new(Vector128.Create(rowIndex, columnIndex, 0, 0), _rows[rowIndex][columnIndex]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal int GetColumnCount(int rowIndex) => _rows[rowIndex].Length;

    private bool TryGetValue(V key, out char value)
    {
        int rowIndex = key[0];
        if (!TryGetValue(_rows, rowIndex, out string? row))
            return None(out value);

        int columnIndex = key[1];
        return TryGetValue(row, columnIndex, out value) || None(out value);
    }

    public bool TryFindKey(char value, out V key)
    {
        var pair = this.FirstOrDefault(it => it.Value == value);
        key = pair.Key;
        return pair.Value == value;
    }

    internal bool IsWalkable(V key) => TryGetValue(key, out char value) && value is not '#';

    private static bool TryGetValue<T>(
        ReadOnlySpan<T> span, int index, [MaybeNullWhen(false)] out T value) =>
        unchecked((uint)index < (uint)span.Length) ? Some(span[index], out value) : None(out value);
}
