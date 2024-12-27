namespace AdventOfCode2024;

public sealed class PartOnePuzzle_Tests
{
    [Theory]
    [InlineData("sample.txt", "4,6,3,5,6,3,5,2,1,0")]
    [InlineData("sample-2.txt", "0,3,5,4,3,0")]
    [InlineData("input.txt", "1,7,2,1,4,1,5,4,0")]
    internal void Solve_ShouldBeEqual(string inputPath, string expected)
    {
        string actual = PartOnePuzzle.Solve(inputPath);
        Assert.Equal(expected, actual);
    }
}
