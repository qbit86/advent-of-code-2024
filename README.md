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
long SingleSimilarityScore(int number) =>
    Math.BigMul(number, countByNumber.GetValueOrDefault(number, 0));
```

## [Day 2: Red-Nosed Reports](https://adventofcode.com/2024/day/2)

[feature/02-red-nosed-reports](https://github.com/qbit86/advent-of-code-2024/tree/feature/02-red-nosed-reports)

## [Day 3: Mull It Over](https://adventofcode.com/2024/day/3)

[feature/03-mull-it-over](https://github.com/qbit86/advent-of-code-2024/tree/feature/03-mull-it-over)

> The .NET SDK (version 7 and later) includes a source generator that recognizes the `GeneratedRegexAttribute` attribute on a partial method that returns `Regex`.
>
> — https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-source-generators

```cs
private static Regex MulRegex => s_regex ??= CreateRegex();

[GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)",
    RegexOptions.Compiled | RegexOptions.CultureInvariant)]
private static partial Regex CreateRegex();
```

```cs
var matches = MulRegex.Matches(line);
```

## [Day 4: Ceres Search](https://adventofcode.com/2024/day/4)

[feature/04-ceres-search](https://github.com/qbit86/advent-of-code-2024/tree/feature/04-ceres-search)

## [Day 5: Print Queue](https://adventofcode.com/2024/day/5)

[feature/05-print-queue](https://github.com/qbit86/advent-of-code-2024/tree/feature/05-print-queue)

The whole input can contain cycles.
But updates seem to be DAGs, otherwise there is no solution.
It looks like the second part of the puzzle can be solved just by sorting pages within updates with a partial and non-transitive comparer for particular inputs.
But in general you need a topological sorting of the pages.

```cs
var outNeighborsMap = edges.ToLookup(it => it.Tail, it => it.Head)
    .ToFrozenDictionary(it => it.Key, it => it.ToFrozenSet());

...

List<int> Sort(IReadOnlyList<int> pages)
{
    List<int> sortedPages = [];
    var inducedOutNeighborsMap = pages.ToFrozenDictionary(
        it => it, it => outNeighborsMap.GetValueOrDefault(it, []).Intersect(pages).ToFrozenSet());
    var graph = ReadOnlyAdjacencyGraph<int>.FromFrozenSets(inducedOutNeighborsMap);
    var handler = CreateDfsHandler(graph);
    handler.FinishVertex += (_, v) => sortedPages.Add(v);
    EagerDfs<int, FrozenSet<int>.Enumerator>.Traverse(graph, pages, handler);
    sortedPages.Reverse();
    return sortedPages;
}
```

Here I used a type inference trick, since the actual types of the `handler` and the `graph` are very complicated.

```cs
private static DfsHandler<int, Endpoints<int>, TGraph> CreateDfsHandler<TGraph>(TGraph _) => new();
```

## [Day 6: Guard Gallivant](https://adventofcode.com/2024/day/6)

[feature/06-guard-gallivant](https://github.com/qbit86/advent-of-code-2024/tree/feature/06-guard-gallivant)

Tried the API for `Vector128<int>` to rotate the direction (<i>x</i>, <i>y</i>) ↦ (<i>y</i>, −<i>x</i>):

```cs
using V = Vector128<int>;

...

private static V Rotate(V direction)
{
    var temp = Vector128.Shuffle(direction, Vector128.Create(1, 0, 2, 3));
    return temp * Vector128.Create(1, -1, 1, 1);
}
```

## [Day 7: Bridge Repair](https://adventofcode.com/2024/day/7)

[feature/07-bridge-repair](https://github.com/qbit86/advent-of-code-2024/tree/feature/07-bridge-repair)

Used depth-first search.

```cs
internal readonly record struct Equation(long TestValue, int[] Numbers) { … }

internal readonly record struct Node(long RunningTotalInclusive, int Index);

…

private static bool CanBeTrue(Equation equation)
{
    Node source = new(equation.Numbers[0], 0);
    Graph graph = new(equation);
    var nodes = EnumerableDfs<Node>.EnumerateVertices(graph, source);
    int lastIndexInclusive = equation.Numbers.Length - 1;
    return nodes.Any(it => it.RunningTotalInclusive == equation.TestValue && it.Index == lastIndexInclusive);
}
```

## [Day 8: Resonant Collinearity](https://adventofcode.com/2024/day/8)

[feature/08-resonant-collinearity](https://github.com/qbit86/advent-of-code-2024/tree/feature/08-resonant-collinearity)

> An antinode occurs at any grid position exactly in line with at least two antennas of the same frequency, regardless of distance.

It's not clear whether we should divide the direction vector on the GCD of its components to get the step vector.

## [Day 9: Disk Fragmenter](https://adventofcode.com/2024/day/9)

[feature/09-disk-fragmenter](https://github.com/qbit86/advent-of-code-2024/tree/feature/09-disk-fragmenter)
