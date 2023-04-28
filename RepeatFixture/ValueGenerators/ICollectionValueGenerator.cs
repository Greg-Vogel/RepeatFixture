namespace RepeatFixture.ValueGenerators;

public abstract class ICollectionValueGenerator
{
    protected Type _type;
    protected int _seed = 0;
    protected string? _propName;
    protected int _currentLevel;
    protected int _maxSubClassFillLevel;
    protected Random _rand;

    public ICollectionValueGenerator(Type type, int seed, string? propName, int currLevel, int maxFillLevel)
    {
        _type = type;
        _seed = GetAdjustedSeed(seed, propName);
        _propName = propName;
        _currentLevel = currLevel + 1;
        _maxSubClassFillLevel = maxFillLevel;
        _rand = new Random(_seed);
    }

    public abstract dynamic GetValue();


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


    public static dynamic? CastToType(Type type, object source)
    {
        string typeName = type.Name;

        // Check our method info cache to see if we already have the method info for this type
        var methodInfo = FixtureLogic.GetCachedMethodInfo(nameof(FixtureLogic.GenericCastToType), type);

        // We do not have it cached, generate it now
        if (methodInfo == null)
        {
            methodInfo = typeof(FixtureLogic).GetMethod("GenericCastToType");
            if (methodInfo == null)
                throw new NullReferenceException($"Method {nameof(FixtureLogic.GenericCastToType)} was not found on class: {nameof(FixtureLogic)}");

            methodInfo = methodInfo.MakeGenericMethod(type);
            // Cache the method info we just generated so we do not have to reflect it again
            FixtureLogic.CacheMethodInfo(nameof(FixtureLogic.GenericCastToType), type, methodInfo);
        }

        return methodInfo.Invoke(null, new[] { source });
    }
}
