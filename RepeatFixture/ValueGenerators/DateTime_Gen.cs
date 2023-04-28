namespace RepeatFixture.ValueGenerators;

internal class DateTime_Gen : ITypeValueGenerator<DateTime>
{
    public DateTime_Gen(int seed, string? propName = null) : base(seed, propName) { }

    public override DateTime GetValue()
    {
        int hour = _rand.Next(0, 23);
        int min = _rand.Next(0, 59);
        int sec = _rand.Next(0, 59);
        int month = _rand.Next(1, 12);
        int day = _rand.Next(1, 28);
        int year = _rand.Next(1950, 2150);

        return new DateTime(year, month,day, hour, min, sec, 0);
    }
}
