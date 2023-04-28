namespace RepeatFixture.ValueGenerators;

internal class Byte_Gen : ITypeValueGenerator<Byte>
{
    public Byte_Gen(int seed, string? propName = null) : base(seed, propName) { }

    public override Byte GetValue()
    {
        return (byte)new Random(_seed).Next(0, byte.MaxValue);
    }
}
