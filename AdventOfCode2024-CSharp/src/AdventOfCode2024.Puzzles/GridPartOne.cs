using System.Collections.Generic;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using static TryHelpers;
using V = Vector128<int>;

internal sealed class GridPartOne : GridBase
{
    internal GridPartOne(string[] map) : base(map) { }

    public override IEnumerator<KeyValuePair<V, char>> GetEnumerator()
    {
        for (int rowIndex = 0; rowIndex < _map.Length; ++rowIndex)
        {
            string row = _map[rowIndex];
            for (int columnIndex = 0; columnIndex < row.Length; ++columnIndex)
                yield return new(Vector128.Create(rowIndex, columnIndex, 0, 0), row[columnIndex]);
        }
    }

    protected override bool TryGetValue(V key, out char value)
    {
        int rowIndex = key[0];
        if (unchecked((uint)rowIndex >= (uint)_map.Length))
            return None(out value);

        string row = _map[rowIndex];
        int columnIndex = key[1];
        if (unchecked((uint)columnIndex >= (uint)row.Length))
            return None(out value);

        value = row[columnIndex];
        return true;
    }
}
