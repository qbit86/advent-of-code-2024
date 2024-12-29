namespace AdventOfCode2024;

public sealed class Keypad_Tests
{
    [Theory]
    [InlineData('A', '0', "<A")]
    [InlineData('0', '2', "^A")]
    [InlineData('9', 'A', "vvvA")]
    public void GetSequence_WhenNumeric_ShouldBeEqual(char startButton, char endButton, string expected)
    {
        string actual = Keypads.Numeric.GetSequence(startButton, endButton);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData('2', '9', 4)]
    public void GetSequenceLength_WhenNumeric_ShouldBeEqual(char startButton, char endButton, int expected)
    {
        int actual = Keypads.Numeric.GetSequence(startButton, endButton).Length;
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData('A', '<', "v<<A")]
    [InlineData('<', 'A', ">>^A")]
    [InlineData('A', '^', "<A")]
    [InlineData('^', 'A', ">A")]
    [InlineData('A', '>', "vA")]
    [InlineData('>', '^', "<^A")]
    [InlineData('^', '^', "A")]
    [InlineData('A', 'v', "<vA")]
    [InlineData('v', 'v', "A")]
    public void GetSequence_WhenDirectional_ShouldBeEqual(char startButton, char endButton, string expected)
    {
        string actual = Keypads.Directional.GetSequence(startButton, endButton);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData('v', 'A', 3)]
    public void GetSequenceLength_WhenDirectional_ShouldBeEqual(char startButton, char endButton, int expected)
    {
        int actual = Keypads.Directional.GetSequence(startButton, endButton).Length;
        Assert.Equal(expected, actual);
    }
}
