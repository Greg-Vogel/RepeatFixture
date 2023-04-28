using System.Text;

namespace RepeatFixture.ValueGenerators;

internal class Boolean_Gen : ITypeValueGenerator<Boolean>
{
    public Boolean_Gen(int seed, string? propName = null) : base(seed, propName) { }

    public override Boolean GetValue()
    {
        return Convert.ToBoolean(_rand.Next(0, 2));
    }
}
