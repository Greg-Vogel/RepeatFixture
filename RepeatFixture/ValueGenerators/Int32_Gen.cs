namespace RepeatFixture.ValueGenerators;

internal class Int32_Gen : ITypeValueGenerator<Int32>
{
    public Int32_Gen(int seed, string? propName = null) : base(seed, propName) { }

    public override Int32 GetValue()
    {
        return _rand.Next(Int32.MinValue, Int32.MaxValue);
    }
}
