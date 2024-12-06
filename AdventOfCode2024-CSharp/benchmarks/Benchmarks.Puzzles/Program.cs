using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

namespace AdventOfCode2024;

using V = Vector128<int>;

internal static class Program
{
    private static void Main()
    {
        var job = new Job(Job.Default)
            .ApplyAndFreeze(RunMode.Short);

        var config = ManualConfig.Create(DefaultConfig.Instance)
            .AddJob(job);
        _ = BenchmarkRunner.Run<VectorBenchmark>(config);
    }
}

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class VectorBenchmark
{
    private readonly V[] _data = [Create(0, 1), Create(1, 0), Create(0, -1), Create(-1, 0)];

    [Benchmark(Baseline = true)]
    public V Swap()
    {
        var result = V.Indices;
        foreach (var v in _data)
            result ^= RotateSwap(v);

        return result;
    }

    [Benchmark]
    public V Shuffle()
    {
        var result = V.Indices;
        foreach (var v in _data)
            result ^= RotateShuffle(v);

        return result;
    }

    [Benchmark]
    public V Cross()
    {
        var result = V.Indices;
        foreach (var v in _data)
            result ^= RotateCross(v);

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static V Create(int e1, int e2) => Vector128.Create(e1, e2, 0, 0);

    private static V RotateSwap(V v) => Vector128.Create(v[1], -v[0], 0, 0);

    private static V RotateShuffle(V v)
    {
        var temp = Vector128.Shuffle(v, Vector128.Create(1, 0, 2, 3));
        return temp * Vector128.Create(1, -1, 1, 1);
    }

    private static V RotateCross(V v) => Cross(v, Vector128.Create(0, 0, 1, 0));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static V Cross(V left, V right)
    {
        var addend = Vector128.Shuffle(left, Vector128.Create(1, 2, 0, 3)) *
            Vector128.Shuffle(right, Vector128.Create(2, 0, 1, 3));
        return -Vector128.Shuffle(left, Vector128.Create(2, 0, 1, 3)) *
            Vector128.Shuffle(right, Vector128.Create(1, 2, 0, 3)) + addend;
    }
}
