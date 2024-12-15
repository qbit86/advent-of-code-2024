using System;
using System.IO;
using System.Reflection;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

public static class Helpers
{
    private static readonly Vector128<int> s_size = Vector128.Create(101, 103, 0, 0);
    private static string? s_outputDirectory;

    internal static Vector128<int> Size => s_size;

    public static Vector128<long> LongSize { get; } = Vector128.Create(s_size[0], s_size[1]);

    private static string OutputDirectory => s_outputDirectory ??= CreateOutputDirectoryPath();

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
        string basename = $"{timestamp.DayOfYear}_{timestamp:HH-mm-ss}";
        return Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            Assembly.GetExecutingAssembly().GetName().Name,
            basename);
    }

    private static Vector128<long> Rem(Vector128<long> dividend, Vector128<long> divisor)
    {
        var quotient = dividend / divisor;
        return dividend - divisor * quotient;
    }

    internal static Vector128<long> Mod(Vector128<long> dividend, Vector128<long> divisor)
    {
        var reminder = Rem(dividend, divisor);
        return Rem(reminder + divisor, divisor);
    }
}
