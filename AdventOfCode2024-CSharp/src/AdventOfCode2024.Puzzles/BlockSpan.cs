namespace AdventOfCode2024;

internal readonly record struct BlockSpan(int Id, int Start, int Length)
{
    internal bool IsFile => Id >= 0;

    internal bool IsFree => Id < 0;
}
