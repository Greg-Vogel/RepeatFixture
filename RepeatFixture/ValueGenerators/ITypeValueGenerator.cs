namespace RepeatFixture.ValueGenerators;

public abstract class ITypeValueGenerator<T>
{
    protected int _seed = 0;
    protected string? _propName;
    protected Random _rand;
    public ITypeValueGenerator(int seed, string? propName = null)
    {
        _seed = GetAdjustedSeed(seed, propName);
        _propName = propName;
        _rand = new Random(_seed);
    }

    public abstract T GetValue();


    private int GetAdjustedSeed(int seed, string? propName = null)
    {
        if (string.IsNullOrEmpty(propName))
            return seed;

        long propNameAdjustedSeed = seed;
        foreach (var c in propName)
        {
            propNameAdjustedSeed += (int)c;
            if (propNameAdjustedSeed > int.MaxValue)
                propNameAdjustedSeed -= int.MaxValue;
        }
        return (int)propNameAdjustedSeed;
    }
}
