using System.Text;

namespace RepeatFixture.ValueGenerators;

internal class String_Gen : ITypeValueGenerator<string>
{
    public String_Gen(int seed, string? propName = null) : base(seed, propName) { }

    public override string GetValue()
    {
        var sb = new StringBuilder();
        sb.Append(string.IsNullOrWhiteSpace(_propName) ? "StringValue" : _propName);
        sb.Append("_");
        sb.Append(_rand.Next(0, int.MaxValue));
        return sb.ToString();
    }
}
