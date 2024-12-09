using System.Runtime.CompilerServices;

namespace AdventOfCode2024;

internal static class TryHelpers
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool None<T>(out T? value)
    {
        value = default;
        return false;
    }
}
