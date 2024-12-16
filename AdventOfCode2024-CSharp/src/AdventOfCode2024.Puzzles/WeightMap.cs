using Arborescence;
using static AdventOfCode2024.TryHelpers;

namespace AdventOfCode2024;

internal readonly struct WeightMap : IPartialReadOnlyDictionary<Endpoints<Node>, int>
{
    public bool TryGetValue(Endpoints<Node> key, out int value)
    {
        if (key.Head.Tile == key.Tail.Tile && key.Head.Direction != key.Tail.Direction)
            return Some(1000, out value);

        if (key.Head.Tile != key.Tail.Tile && key.Head.Direction == key.Tail.Direction)
            return Some(1, out value);

        value = int.MaxValue;
        return false;
    }
}
