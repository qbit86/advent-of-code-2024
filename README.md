# Advent of Code 2024

Solutions are organized as separate branches in the repository, not as subfolders within a single branch.

## [Day 1: Historian Hysteria](https://adventofcode.com/2024/day/1)

[feature/01-historian-hysteria](https://github.com/qbit86/advent-of-code-2024/tree/feature/01-historian-hysteria)

> New methods `CountBy` and `AggregateBy` have been introduced.
> These methods make it possible to aggregate state by key without needing to allocate intermediate groupings via `GroupBy`.
>
> — https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/libraries#linq

```cs
var countByNumber = rightNumbers.CountBy(it => it).ToFrozenDictionary();
```

Tried the [Math.BigMul](https://learn.microsoft.com/en-us/dotnet/api/system.math.bigmul?view=net-9.0#system-math-bigmul(system-int32-system-int32)) API for the first time.

```cs
long AggregateFunc(long acc, int number) =>
    acc + Math.BigMul(number, countByNumber.GetValueOrDefault(number, 0));
```
