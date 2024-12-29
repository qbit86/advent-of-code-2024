using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024;

using V = Vector128<int>;

public sealed class Keypad
{
    private readonly Dictionary<(char Start, char End), string> _cache = [];

    private Keypad(FrozenDictionary<V, char> buttonByPosition, FrozenDictionary<char, V> positionByButton)
    {
        ButtonByPosition = buttonByPosition;
        PositionByButton = positionByButton;
    }

    public FrozenDictionary<V, char> ButtonByPosition { get; }

    public FrozenDictionary<char, V> PositionByButton { get; }

    public static Keypad Create(IReadOnlyList<string> grid)
    {
        ArgumentNullException.ThrowIfNull(grid);
        List<KeyValuePair<char, V>> buttonPositionPairs = new(grid.Count);
        for (int i = 0; i < grid.Count; ++i)
        {
            string row = grid[i];
            for (int j = 0; j < row.Length; ++j)
            {
                var position = Vector128.Create(i, j, 0, 0);
                char button = row[j];
                buttonPositionPairs.Add(new(button, position));
            }
        }

        var buttonByPosition = buttonPositionPairs.ToFrozenDictionary(it => it.Value, it => it.Key);
        var positionByButton = buttonPositionPairs.ToFrozenDictionary();
        return new(buttonByPosition, positionByButton);
    }

    public string GetSequence(char startButton, char endButton)
    {
        if (_cache.TryGetValue((startButton, endButton), out string? cachedResult))
            return cachedResult;

        string result = GetSequenceUnchecked(startButton, endButton);
        _cache.Add((startButton, endButton), result);
        return result;
    }

    private string GetSequenceUnchecked(char startButton, char endButton)
    {
        var startPosition = PositionByButton[startButton];
        var endPosition = PositionByButton[endButton];
        var direction = endPosition - startPosition;
        var (left, right) = Order(startPosition, direction);
        return new string(left.Button, left.Count) + new string(right.Button, right.Count) + 'A';
    }

    private static Repeat GetHorizontal(int distance)
    {
        char button = distance switch
        {
            < 0 => '<',
            > 0 => '>',
            _ => '\0'
        };
        return new(button, Math.Abs(distance));
    }

    private static Repeat GetVertical(int distance)
    {
        char button = distance switch
        {
            < 0 => '^',
            > 0 => 'v',
            _ => '\0'
        };
        return new(button, Math.Abs(distance));
    }

    private (Repeat, Repeat) Order(V startPosition, V direction)
    {
        var horizontal = GetHorizontal(direction[1]);
        var vertical = GetVertical(direction[0]);
        var (left, right) = OrderUnchecked(horizontal, vertical);
        var firstDirection = left.Button is '<' or '>'
            ? direction * Vector128.Create(0, 1, 0, 0)
            : direction * Vector128.Create(1, 0, 0, 0);
        var corner = startPosition + firstDirection;
        return ButtonByPosition[corner] is '_' ? (right, left) : (left, right);
    }

    private static (Repeat, Repeat) OrderUnchecked(Repeat horizontal, Repeat vertical) =>
        horizontal.Button is '<' ? (horizontal, vertical) : (vertical, horizontal);
}
