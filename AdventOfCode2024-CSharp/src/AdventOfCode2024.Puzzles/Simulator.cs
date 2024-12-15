using System;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<long>;

public sealed class Simulator
{
    private readonly V _size;
    private readonly V[] _velocities;

    public Simulator(V size, V[] velocities)
    {
        ArgumentNullException.ThrowIfNull(velocities);

        _size = size;
        _velocities = velocities;
    }

    public void Simulate(ReadOnlySpan<V> sourcePositions, Span<V> destinationPositions)
    {
        for (int i = 0; i < sourcePositions.Length; ++i)
            destinationPositions[i] = Helpers.Mod(sourcePositions[i] + _velocities[i], _size);
    }
}
