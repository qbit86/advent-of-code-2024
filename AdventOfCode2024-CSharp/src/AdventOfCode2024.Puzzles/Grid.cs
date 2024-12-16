using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using static TryHelpers;
using V = Vector128<int>;

internal sealed class Grid : IEnumerable<KeyValuePair<V, char>>
{
    private readonly string[] _map;

    internal Grid(string[] map) => _map = map;

    public IEnumerator<KeyValuePair<V, char>> GetEnumerator()
    {
        for (int rowIndex = 0; rowIndex < _map.Length; ++rowIndex)
        {
            for (int columnIndex = 0; columnIndex < _map.Length; ++columnIndex)
                yield return new(Vector128.Create(rowIndex, columnIndex, 0, 0), _map[rowIndex][columnIndex]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal bool TryGetValue(V key, out char value)
    {
        int rowIndex = key[0];
        if (unchecked((uint)rowIndex >= (uint)_map.Length))
            return None(out value);

        int columnIndex = key[1];
        string row = _map[rowIndex];
        if (unchecked((uint)columnIndex >= (uint)row.Length))
            return None(out value);

        value = row[columnIndex];
        return true;
    }

    internal bool TryFindKey(char value, out V key)
    {
        var pair = this.FirstOrDefault(it => it.Value == value);
        key = pair.Key;
        return pair.Value == value;
    }

    internal bool IsWalkable(V key) => TryGetValue(key, out char value) && value is not '#';
}
