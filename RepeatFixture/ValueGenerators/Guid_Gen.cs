using System.Text;

namespace RepeatFixture.ValueGenerators;

internal class Guid_Gen : ITypeValueGenerator<Guid>
{
    public Guid_Gen(int seed, string? propName = null) : base(seed, propName) { }

    public override Guid GetValue()
    {
        var sb = new StringBuilder();
        sb.Append("abcdef00-0000-0000-0000-000"); // our guids will have a static first part
        sb.Append(_rand.Next(100_000_000, 1_000_000_000)); // and the last 9 digits will be randomly seeded
        return Guid.Parse(sb.ToString());
    }
}
