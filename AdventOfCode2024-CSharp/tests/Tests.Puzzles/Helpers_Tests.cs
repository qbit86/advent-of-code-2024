namespace AdventOfCode2024;

public sealed class Helpers_Tests
{
    [Theory]
    [InlineData(42, 15, 37)]
    internal void Mix_ShouldBeEqual(int secret, int value, int expected)
    {
        int actual = Helpers.Mix(secret, value);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(100000000, 16113920)]
    internal void Prune_ShouldBeEqual(int secret, int expected)
    {
        int actual = Helpers.Prune(secret);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(123, 15887950)]
    [InlineData(15887950, 16495136)]
    [InlineData(16495136, 527345)]
    [InlineData(527345, 704524)]
    internal void SimulateSingle_ShouldBeEqual(int secret, int expected)
    {
        int actual = Helpers.SimulateSingleStep(secret);
        Assert.Equal(expected, actual);
    }
}
