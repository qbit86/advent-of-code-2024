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

The basic data structure for this puzzle is a double-ended queue of block spans.

```cs
internal readonly record struct BlockSpan(int Id, int Start, int Length)
{
    internal bool IsFile => Id >= 0;

    internal bool IsFree => Id < 0;
}
```

## [Day 10: Hoof It](https://adventofcode.com/2024/day/10)

[feature/10-hoof-it](https://github.com/qbit86/advent-of-code-2024/tree/feature/10-hoof-it)

Depth-first search and dynamic programming.
The only trick is to share the cache per trailhead, not per pair (trailhead, destination).
To achieve this, I traversed the topographic map forwards to find the trail ends for each trailhead, then backwards during the “dynamic” part.

## [Day 11: Plutonian Pebbles](https://adventofcode.com/2024/day/11)

[feature/11-plutonian-pebbles](https://github.com/qbit86/advent-of-code-2024/tree/feature/11-plutonian-pebbles)

Dynamic programming using the cache with the `Node` as the key.

```cs
internal readonly record struct Node(long Stone, int RemainingBlinks);
```

## [Day 12: Garden Groups](https://adventofcode.com/2024/day/12)

[feature/12-garden-groups](https://github.com/qbit86/advent-of-code-2024/tree/feature/12-garden-groups)

The most difficult part is dealing with the perimeter.
From the following graph definition, I filter all bound edges that "stick out" for each region (connected component).

```cs
public IEnumerator<V> EnumerateOutNeighbors(V vertex)
{
    if (!_grid.TryGetValue(vertex, out char kind) || kind != _kind)
        yield break;
    foreach (var direction in Helpers.Directions)
        yield return vertex + direction; // No bound checks here!
}
```

Then I build a meta-graph whose nodes are such bound edges pointing out of regions.
And a side of a region is a connected component in such a meta-graph.

## [Day 13: Claw Contraption](https://adventofcode.com/2024/day/13)

[feature/13-claw-contraption](https://github.com/qbit86/advent-of-code-2024/tree/feature/13-claw-contraption)

To solve a system of linear equations I use [Cramers's rule](https://en.wikipedia.org/wiki/Cramer%27s_rule).

A fancy way of calculating the determinant:
```cs
using V = Vector128<long>;

…

private static long Det(V left, V right)
{
    var rightReordered = Vector128.Shuffle(right, Vector128.Create(1, 0));
    var product = left * rightReordered;
    return Vector128.Sum(Vector128.Create(1, -1) * product);
}
```

## [Day 14: Restroom Redoubt](https://adventofcode.com/2024/day/14)

[feature/14-restroom-redoubt](https://github.com/qbit86/advent-of-code-2024/tree/feature/14-restroom-redoubt)

I optimize the score, where the score is the number of robots with two other robots in their [Von Neumann neighborhood](https://en.wikipedia.org/wiki/Von_Neumann_neighborhood).
The other idea was to optimize the score, defined as the information entropy of the robots' positions:
$-\sum_{i} \Pr(x_i) \log_2 \Pr(x_i)$.

## [Day 15: Warehouse Woes](https://adventofcode.com/2024/day/15)

[feature/15-warehouse-woes](https://github.com/qbit86/advent-of-code-2024/tree/feature/15-warehouse-woes)

I ustilized `ImmutableHashSet<V>` for box positions to simplify the simulation.

## [Day 16: Reindeer Maze](https://adventofcode.com/2024/day/16)

[feature/16-reindeer-maze](https://github.com/qbit86/advent-of-code-2024/tree/feature/16-reindeer-maze)

The trick is to define a graph that operates not on tiles of the map, but instead on _nodes_, where the node is a pair (_tile_, _direction_).
Each node then has three candidate edges: move, turn left, turn right.
These edges are weighted, so we can apply Dijkstra's algorithm.

For the second part, I define a reversed graph, and traverse from `E` to `S`.
Then I count the nodes, for which the sum of their distances from the path endpoints is the known score.

## [Day 17: Chronospatial Computer](https://adventofcode.com/2024/day/17)

[feature/17-chronospatial-computer](https://github.com/qbit86/advent-of-code-2024/tree/feature/17-chronospatial-computer)

Tracing the program reveals that it is a simple loop with a single break.
Each 3-bit value at the output depends on at most 10 bits of the current value of register A.
This observation allows us to narrow our search using the backtracking technique instead of brute force.

## [Day 18: RAM Run](https://adventofcode.com/2024/day/18)

[feature/18-ram-run](https://github.com/qbit86/advent-of-code-2024/tree/feature/18-ram-run)

Breadth-first search for pathfinding, and binary search to determine the first byte that will cut off the path to the exit.

## [Day 19: Linen Layout](https://adventofcode.com/2024/day/19)

[feature/19-linen-layout](https://github.com/qbit86/advent-of-code-2024/tree/feature/19-linen-layout)

I solved it without using a [trie](https://en.wikipedia.org/wiki/Trie), just dynamic programming on offsets within the design sequence.

## [Day 20: Race Condition](https://adventofcode.com/2024/day/20)

[feature/20-race-condition](https://github.com/qbit86/advent-of-code-2024/tree/feature/20-race-condition)

No need to traverse the modified graphs with “tunnels”.
The racetrack has no branches.
We can prebuild the distance map and the path.
Then, for each position on the track, we enumerate its Von Neumann neighborhood as candidates for the end positions of the cheats, and compare the new Manhattan distance with the one stored in the fair distance map.

## [Day 21: Keypad Conundrum](https://adventofcode.com/2024/day/21)

[feature/21-keypad-conundrum](https://github.com/qbit86/advent-of-code-2024/tree/feature/21-keypad-conundrum)

Let's look at an execution trace from an example.
```
<vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A
v<<A>>^A<A>AvA<^AA>A<vAAA>^A
<A^A>^^AvvvA
029A
```

For clarity, I have aligned all the button presses on the current level with the `A` keystrokes on the previous level.
```
<vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A
  v <<   A >>  ^ A   <   A > A  v  A   <  ^ AA > A   < v  AAA >  ^ A
         <       A       ^   A     >        ^^   A        vvv      A
                 0           2                   9                 A
```

We measure the distance between the buttons on the keypad in terms of the number of button presses on the directional keypad of the previous level.
It turns out that the shortest path in such a metric is never a zigzag, but always a corner: └ or ┐, ┌ or ┘.
But which one?
When you need to wrap around the gap on the keypad, you have only one option.
Otherwise, the preferred order for each diagonal direction is
- `↘`: `v>` (is better than `>v`)
- `↖`: `<^` (is better than `^<`)
- `↙`: `<v` (is better than `v<`)
- `↗`: `^>` (is better than `>^`)

## [Day 22: Monkey Market](https://adventofcode.com/2024/day/22)

[feature/22-monkey-market](https://github.com/qbit86/advent-of-code-2024/tree/feature/22-monkey-market)

I used the MoreLINQ library for a sliding window of five elements.

## [Day 23: LAN Party](https://adventofcode.com/2024/day/23)

[feature/23-lan-party](https://github.com/qbit86/advent-of-code-2024/tree/feature/23-lan-party)

The [Bron–Kerbosch algorithm](https://en.wikipedia.org/wiki/Bron%E2%80%93Kerbosch_algorithm) for finding the maximal clique in an undirected graph.

## [Day 24: Crossed Wires](https://adventofcode.com/2024/day/24)

[feature/24-crossed-wires](https://github.com/qbit86/advent-of-code-2024/tree/feature/24-crossed-wires)

We need to find out which implicit rules are violated by which components of the malfunctioning [adder](https://en.wikipedia.org/wiki/Adder_(electronics)).
- Each z-wire (except the last one) must be connected to an XOR gate.
- Each OR gate must have its inputs connected to AND gates.
- Each XOR gate must be connected to an x, y, or z-wire.
- Each XOR gate whose output is a z-wire (except `z01`) must not have an input from an AND gate.

## [Day 25: Code Chronicle](https://adventofcode.com/2024/day/25)

[feature/25-code-chronicle](https://github.com/qbit86/advent-of-code-2024/tree/feature/25-code-chronicle)
