namespace AdventOfCode2024;

public static class Helpers
{
    public static int Mix(int secret, int value) => secret ^ value;

    public static int Prune(int secret)
    {
        const int mask = (1 << 24) - 1;
        return secret & mask;
    }

    public static int SimulateSingleStep(int secret)
    {
        int leftShifted = secret << 6;
        secret = Mix(secret, leftShifted);
        secret = Prune(secret);
        int rightShifted = secret >> 5;
        secret = Mix(secret, rightShifted);
        secret = Prune(secret);
        int otherShifted = secret << 11;
        secret = Mix(secret, otherShifted);
        secret = Prune(secret);
        return secret;
    }
}
