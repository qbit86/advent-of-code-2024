using System.Collections;
using System.Collections.Generic;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using static TryHelpers;
using P = Vector128<int>;

internal sealed class Grid : IEnumerable<KeyValuePair<P, char>>
{
    private readonly string[] _rows;

    internal Grid(string[] rows) => _rows = rows;

    public IEnumerator<KeyValuePair<P, char>> GetEnumerator()
    {
        for (int rowIndex = 0; rowIndex < _rows.Length; ++rowIndex)
        {
            string row = _rows[rowIndex];
            for (int columnIndex = 0; columnIndex < row.Length; ++columnIndex)
            {
                var key = Vector128.Create(rowIndex, columnIndex, 0, 0);
                yield return KeyValuePair.Create(key, _rows[rowIndex][columnIndex]);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal bool Contains(P key) => TryGetValue(key, out _);

    internal bool TryGetValue(P key, out char value)
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
}
