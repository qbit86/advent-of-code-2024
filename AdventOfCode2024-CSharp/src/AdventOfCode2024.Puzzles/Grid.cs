using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using static TryHelpers;
using V = Vector128<int>;

internal sealed class Grid
{
    private readonly string[] _rows;

    internal Grid(string[] rows) => _rows = rows;

    internal bool TryGetValue(V position, out char value)
    {
        if (unchecked((uint)position[0]) >= (uint)_rows.Length)
            return None(out value);

        string row = _rows[position[0]];
        if (unchecked((uint)position[1]) >= (uint)row.Length)
            return None(out value);

        value = row[position[1]];
        return true;
    }
}
