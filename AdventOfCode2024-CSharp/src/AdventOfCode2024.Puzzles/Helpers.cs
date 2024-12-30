using System;
using System.IO;
using System.Reflection;

namespace AdventOfCode2024;

internal static class Helpers
{
    private static string? s_outputDirectory;

    private static string OutputDirectory => s_outputDirectory ??= CreateOutputDirectoryPath();

    internal static string EnsureOutputDirectory()
    {
        string outputDirectory = OutputDirectory;
        if (!Directory.Exists(outputDirectory))
            _ = Directory.CreateDirectory(outputDirectory);
        return outputDirectory;
    }

    private static string CreateOutputDirectoryPath()
    {
        var timestamp = DateTime.Now;
        string basename = $"{timestamp.DayOfYear}";
        return Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            Assembly.GetExecutingAssembly().GetName().Name,
            basename);
    }
}
