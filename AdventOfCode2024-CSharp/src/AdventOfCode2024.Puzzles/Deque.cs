using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2024;

using static TryHelpers;

internal sealed class Deque<T> : IReadOnlyList<T>
{
    private readonly T[] _array;
    private int _end;
    private int _start;

    private Deque(T[] array, int start, int end)
    {
        _array = array;
        _start = start;
        _end = end;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = _start; i < _end; ++i)
            yield return _array[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => _end - _start;

    public T this[int index]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
            return _array[_start + index];
        }
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
            _array[_start + index] = value;
        }
    }

    public static Deque<T> Create<TCollection>(TCollection items)
        where TCollection : IEnumerable<T>
    {
        if (items is null)
            throw new ArgumentNullException(nameof(items));

        var array = items.ToArray();
        return new(array, 0, array.Length);
    }

    public bool TryAdd(T item)
    {
        bool canAdd = _end < _array.Length;
        if (canAdd)
            _array[_end++] = item;
        return canAdd;
    }

    public bool TryTake([MaybeNullWhen(false)] out T item)
    {
        if (_end <= _start)
            return None(out item);
        item = _array[--_end];
        return true;
    }

    public bool TryAddFront(T item)
    {
        bool canAdd = _start > 0;
        if (canAdd)
            _array[--_start] = item;
        return canAdd;
    }

    public bool TryTakeFront([MaybeNullWhen(false)] out T item)
    {
        if (_end <= _start)
            return None(out item);
        item = _array[_start++];
        return true;
    }
}
