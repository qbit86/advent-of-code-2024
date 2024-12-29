using System;

namespace AdventOfCode2024;

public static class Keypads
{
    [ThreadStatic] private static Keypad? s_numeric;

    [ThreadStatic] private static Keypad? s_directional;

    public static Keypad Numeric => s_numeric ??= Keypad.Create(["789", "456", "123", "_0A"]);

    public static Keypad Directional => s_directional ??= Keypad.Create(["_^A", "<v>"]);
}
