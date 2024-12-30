namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", "co,de,ka,ta")]
    [InlineData("input.txt", "cc,ff,fh,fr,ny,oa,pl,rg,uj,wd,xn,xs,zw")]
    internal void Solve_ShouldBeEqual(string inputPath, string expected)
    {
        string actual = PartTwoPuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
