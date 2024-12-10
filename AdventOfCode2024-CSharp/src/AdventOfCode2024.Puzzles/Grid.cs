using System.Collections;
using System.Collections.Generic;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using static TryHelpers;
using P = Vector128<int>;

internal sealed class Grid : IEnumerable<KeyValuePair<P, int>>
{
    private readonly string[] _rows;

    public Grid(string[] rows) => _rows = rows;

    public IEnumerator<KeyValuePair<P, int>> GetEnumerator()
    {
        for (int rowIndex = 0; rowIndex < _rows.Length; ++rowIndex)
        {
            string row = _rows[rowIndex];
            for (int columnIndex = 0; columnIndex < row.Length; ++columnIndex)
            {
                char c = row[columnIndex];
                if (char.IsDigit(c))
                    yield return KeyValuePair.Create(Helpers.Create(rowIndex, columnIndex), c - '0');
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal int GetHeightOrDefault(P key, int defaultHeight = -1) =>
        TryGetHeight(key, out int height) ? height : defaultHeight;

    internal bool TryGetHeight(P key, out int height)
    {
        if (unchecked((uint)key[0] >= (uint)_rows.Length))
            return None(out height);
        string row = _rows[key[0]];
        if (unchecked((uint)key[1] >= (uint)row.Length))
            return None(out height);
        char c = row[key[1]];
        height = c - '0';
        return char.IsDigit(c);
    }
}
