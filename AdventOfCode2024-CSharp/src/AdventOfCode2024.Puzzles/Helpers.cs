using System;
using System.Diagnostics;

namespace AdventOfCode2024;

internal static class Helpers
{
    internal static int ComputeDigitCount(long n)
    {
        Debug.Assert(n >= 0);
        return n is 0 ? 1 : (int)Math.Floor(Math.Log10(n)) + 1;
    }
}
