using System.Collections;
using System.Collections.Generic;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal abstract class GridBase : IEnumerable<KeyValuePair<V, char>>
{
    protected readonly string[] _map;

    protected GridBase(string[] map) => _map = map;

    public abstract IEnumerator<KeyValuePair<V, char>> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    protected abstract bool TryGetValue(V key, out char value);

    internal bool IsWalkable(V key) => TryGetValue(key, out char value) && value is not '#';
}
