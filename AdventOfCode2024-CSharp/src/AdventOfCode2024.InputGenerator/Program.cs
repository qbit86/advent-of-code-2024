using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace AdventOfCode2024;

internal static class Program
{
    private static string? s_outputDirectory;

    private static string OutputDirectory => s_outputDirectory ??= CreateOutputDirectoryPath();

    private static void Main()
    {
        int[] items = [1, 2, 3, 4, 5];
        string outputDirectory = EnsureOutputDirectory();
        string path = Path.Join(outputDirectory, "sample-2.txt");
        Console.WriteLine(path);
        using var stream = File.OpenWrite(path);
        using StreamWriter writer = new(stream);
        for (int i = 0; i < items.Length - 1; i++)
            writer.WriteLine($"{items[i]}|{items[i + 1]}");

        writer.WriteLine();

        do
        {
            writer.WriteLine(string.Join(',', items));
        } while (NextPermutation(items));

        writer.Flush();
        stream.Flush();
    }

    /// <see href="https://en.wikipedia.org/wiki/Permutation#Generation_in_lexicographic_order" />
    private static bool NextPermutation<T>(T[] items)
        where T : IComparisonOperators<T, T, bool>
    {
        var indexedItems = items.Index().ToArray();
        (int k, var item) = indexedItems.SkipLast(1).LastOrDefault(Predicate, (-1, default!));
        if (k < 0)
            return false;

        var other = indexedItems.Skip(k).Last(it => item < it.Item);
        int l = other.Index;

        (items[k], items[l]) = (items[l], items[k]);
        items.AsSpan(k + 1).Reverse();

        return true;

        bool Predicate((int Index, T Item) p) => p.Item < items[p.Index + 1];
    }

    public static string EnsureOutputDirectory()
    {
        string outputDirectory = OutputDirectory;
        if (!Directory.Exists(outputDirectory))
            Directory.CreateDirectory(outputDirectory);
        return outputDirectory;
    }

    private static string CreateOutputDirectoryPath()
    {
        var timestamp = DateTime.Now;
        string name = $"{timestamp.DayOfYear}_{timestamp:HH-mm-ss}";
        return Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            Assembly.GetExecutingAssembly().GetName().Name,
            timestamp.ToString(name, CultureInfo.InvariantCulture));
    }
}
