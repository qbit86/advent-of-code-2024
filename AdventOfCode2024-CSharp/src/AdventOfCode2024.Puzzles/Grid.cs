using System.Collections;
using System.Collections.Generic;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using static Helpers;
using static TryHelpers;
using V = Vector128<int>;

internal sealed class Grid : IEnumerable<KeyValuePair<V, char>>
{
    private readonly string[] _rows;

    internal Grid(string[] rows) => _rows = rows;

    internal char this[V key]
    {
        get
        {
            if (!TryGetValue(key, out char value))
                throw new KeyNotFoundException();
            return value;
        }
    }

    public IEnumerator<KeyValuePair<V, char>> GetEnumerator()
    {
        for (int i = 0; i < _rows.Length; ++i)
        {
            string row = _rows[i];
            for (int j = 0; j < row.Length; ++j)
            {
                var key = Create(i, j);
                char value = row[j];
                yield return KeyValuePair.Create(key, value);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal bool ContainsKey(V key) => TryGetValue(key, out _);

    internal bool TryGetValue(V key, out char value)
    {
        int rowIndex = key[0];
        if (unchecked((uint)rowIndex >= (uint)_rows.Length))
            return None(out value);

        string row = _rows[rowIndex];
        int columnIndex = key[1];
        if (unchecked((uint)columnIndex >= (uint)row.Length))
            return None(out value);

        return Some(row[columnIndex], out value);
    }

    internal char GetValueOrDefault(V key) => GetValueOrDefault(key, '\0');

    private char GetValueOrDefault(V key, char defaultValue) =>
        TryGetValue(key, out char value) ? value : defaultValue;
}
