namespace RepeatFixture.ValueGenerators;

internal class DateOnly_Gen : ITypeValueGenerator<DateOnly>
{
    public DateOnly_Gen(int seed, string? propName = null) : base(seed, propName) { }

    public override DateOnly GetValue()
    {
        int month = _rand.Next(1, 12);
        int day = _rand.Next(1, 28);
        int year = _rand.Next(1950, 2150);

        return new DateOnly(year, month, day);
    }
}
