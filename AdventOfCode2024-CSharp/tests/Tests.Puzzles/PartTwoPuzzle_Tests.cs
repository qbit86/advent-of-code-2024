namespace AdventOfCode2024;

public sealed class PartTwoPuzzle_Tests
{
    [Theory]
    [InlineData("input.txt", "gjc,gvm,qjj,qsb,wmp,z17,z26,z39")]
    internal void Solve_ShouldBeEqual(string inputPath, string expected)
    {
        string actual = PartTwoPuzzle.SolveString(inputPath);
        Assert.Equal(expected, actual);
    }
}
