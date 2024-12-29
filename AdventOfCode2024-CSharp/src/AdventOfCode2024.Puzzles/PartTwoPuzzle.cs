using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;
using MoreLinq;

namespace AdventOfCode2024;

using WindowByChanges = FrozenDictionary<Vector128<int>, Window>;

public static class PartTwoPuzzle
{
    private const int PriceCount = 2000;
    private const int WindowSize = 5;

    public static long Solve(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        string[] lines = File.ReadAllLines(path);
        return Solve(lines);
    }

    private static long Solve(string[] rows)
    {
        int[] seeds = rows.Select(it => int.Parse(it, CultureInfo.InvariantCulture)).ToArray();
        return Solve(seeds);
    }

    private static long Solve(int[] seeds)
    {
        var buyers = Enumerable.Range(0, seeds.Length);
        var windowByChangesByBuyer = buyers.Select(it => CreateWindowByChanges(seeds[it])).ToArray();
        var changes = windowByChangesByBuyer.SelectMany(it => it.Keys).Distinct();
        var changeTotalPairs = changes.Select(change => KeyValuePair.Create(change,
            buyers.Select(it => GetPrice(windowByChangesByBuyer[it], change)).Sum()));
        var bestChangeTotalPair = changeTotalPairs.MaxBy(pair => pair.Value);
        return bestChangeTotalPair.Value;
    }

    private static int GetPrice(WindowByChanges windowByChanges, Vector128<int> change) =>
        windowByChanges.TryGetValue(change, out var window) ? window.Price : 0;

    private static WindowByChanges CreateWindowByChanges(int seed)
    {
        var windows = CreateWindows(seed).Take(PriceCount - WindowSize + 1);
        var lookup = windows.ToLookup(it => it.Changes);
        var earliestWindows = lookup.Select(g => g.MinBy(it => it.Index));
        return earliestWindows.ToFrozenDictionary(it => it.Changes);
    }

    public static IEnumerable<int> EnumeratePrices(int seed)
    {
        int secret = seed;
        while (true)
        {
            yield return secret % 10;
            secret = Helpers.SimulateSingleStep(secret);
        }
    }

    public static IEnumerable<Window> CreateWindows(int seed)
    {
        var prices = EnumeratePrices(seed);
        return CreateWindows(prices);
    }

    private static IEnumerable<Window> CreateWindows(IEnumerable<int> prices)
    {
        var indexedPrices = Enumerable.Index(prices);
        var windowLists = indexedPrices.Window(WindowSize);
        return windowLists.Select(Create);
    }

    private static Window Create(IList<(int Index, int Item)> indexedPricesWindow)
    {
        int index = indexedPricesWindow[^1].Index;
        int price = indexedPricesWindow[^1].Item;
        var changes = Vector128.Create(
            indexedPricesWindow[1].Item - indexedPricesWindow[0].Item,
            indexedPricesWindow[2].Item - indexedPricesWindow[1].Item,
            indexedPricesWindow[3].Item - indexedPricesWindow[2].Item,
            indexedPricesWindow[4].Item - indexedPricesWindow[3].Item);
        return new(index, price, changes);
    }
}
