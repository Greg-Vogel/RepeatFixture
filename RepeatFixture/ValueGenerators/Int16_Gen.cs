namespace RepeatFixture.ValueGenerators;

internal class Int16_Gen : ITypeValueGenerator<Int16>
{
    public Int16_Gen(int seed, string? propName = null) : base(seed, propName) { }

    public override Int16 GetValue()
    {
        return (Int16)_rand.Next(Int16.MinValue, Int16.MaxValue);
    }
}
