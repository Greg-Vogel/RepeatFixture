namespace RepeatFixture;

public class RepeatFixture
{
    /// <summary>
    /// Sets the level of nested subclasses to generate
    /// </summary>
    /// <param name="level">Maximum nest level to populate</param>
    public void SetSubClassFillLevel(int level)
    {
        FixtureLogic.DefaultSubClassFillLevel = level;
    }

    /// <summary>
    /// </summary>
    /// <summary>
    /// If the Create method is filling custom classes (sub classes) that are not desired, register all namespaces of subclasses you want populated here.
    /// </summary>
    /// <param name="ns"></param>
    public static void RegisterNamespaces(params string[] namespaces)
    {
        if (FixtureLogic._namespaces == null)
            FixtureLogic._namespaces = new List<string>();

        namespaces.ToList().ForEach(ns => { if (!FixtureLogic._namespaces.Contains(ns)) FixtureLogic._namespaces.Add(ns); });
    }

    /// <summary>
    /// Creates multiple of the specified Type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subClassFillLevel"></param>
    /// <returns></returns>
    public static List<T> CreateMany<T>(int subClassFillLevel)
    {
        return CreateMany<T>(3, subClassFillLevel);
    }

    /// <summary>
    /// Creates multiple of the specified Type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="count">By Default it is set to 3</param>
    /// <param name="subClassFillLevel"></param>
    /// <returns></returns>
    public static List<T> CreateMany<T>(int count = 3, int subClassFillLevel = int.MinValue)
    {
        count = Math.Abs(count);
        List<T> list = new List<T>();
        for (int i = 0; i < count; i++)
        {
            list.Add(FixtureLogic.CreateInternal<T>(int.MinValue + i, subClassFillLevel, 1));
        }

        return list;
    }

    /// <summary>
    /// Creates a single instance of defined type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subClassFillLevel"></param>
    /// <returns></returns>
    public static T Create<T>(int subClassFillLevel)
    {
        return FixtureLogic.CreateInternal<T>(0, subClassFillLevel, 1);
    }

    /// <summary>
    /// Creates a single instance of the defined type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="seed"></param>
    /// <param name="subClassFillLevel"></param>
    /// <returns></returns>
    public static T Create<T>(int seed = 0, int subClassFillLevel = int.MinValue)
    {
        return FixtureLogic.CreateInternal<T>(seed, subClassFillLevel, 1);
    }
        
}
