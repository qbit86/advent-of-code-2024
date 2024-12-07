using System.Collections.Immutable;
using System.Text;

namespace AdventOfCode2024;

internal readonly record struct Node(long RunningTotalInclusive, int Index, ImmutableArray<char> Operators)
{
    private bool PrintMembers(StringBuilder builder)
    {
        _ = builder.Append($"{nameof(RunningTotalInclusive)} = ").Append(RunningTotalInclusive);
        _ = builder.Append($", {nameof(Index)} = ").Append(Index);
        _ = builder.Append($", {nameof(Operators)} = [").Append(string.Join(", ", Operators)).Append(']');
        return true;
    }
}
