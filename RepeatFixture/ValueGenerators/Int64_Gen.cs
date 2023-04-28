namespace RepeatFixture.ValueGenerators;

internal class Int64_Gen : ITypeValueGenerator<Int64>
{
    public Int64_Gen(int seed, string? propName = null) : base(seed, propName) { }

    public override Int64 GetValue()
    {
        Int64 randVal = _rand.Next(Int32.MinValue, Int32.MaxValue);

        // make sure our value is in the Int64 range and tests out the values greater than Int32 supports
        if (randVal < 0)
        {
            randVal -= int.MaxValue;
        }
        else
        {
            randVal += int.MinValue;
        }
        randVal *= 2;

        return randVal;
    }
}
