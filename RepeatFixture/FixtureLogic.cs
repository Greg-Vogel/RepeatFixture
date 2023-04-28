using RepeatFixture.ValueGenerators;
using System.Linq.Expressions;
using System.Reflection;

namespace RepeatFixture;

internal class FixtureLogic
{
    internal static List<string>? _namespaces;

    internal static int DefaultSubClassFillLevel = 1;



    /// <summary>
    /// Creates a single anonymous object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="seed"></param>
    /// <returns></returns>
    public static T CreateInternal<T>(int seed = 0, int subClassFillLevel = int.MinValue, int curLevel = 1)
    {
        if (subClassFillLevel == int.MinValue)
            subClassFillLevel = DefaultSubClassFillLevel;

        // Get an instance of the object
        var obj = GenObject<T>();

        var seedThisLevel = seed + curLevel;

        // identify all public properties on the object
        Type type = typeof(T);
        var properties = type.GetProperties();

        // loop through each property and set a value for it if needed
        foreach (var prop in properties)
        {
            dynamic? propValue;
            try
            {
                // We need to generate a value for this property....
                // generate a random value for this property
                if (GenValue(prop.PropertyType, curLevel, (int)subClassFillLevel, out propValue, seedThisLevel, prop.Name))
                {
                    // assign that value back to the current property
                    prop.SetValue(obj, propValue);
                    continue;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to Generate a Value for property: {prop.Name} on type: {type.Name}", ex);
            }

            // The value was not a known or handled type... most likely a custom class
            //    If it does not match certain values indicating it is a custom class, just skip it
            if (!prop.PropertyType.IsClass || (_namespaces != null && prop.PropertyType.Namespace != null && !_namespaces.Contains(prop.PropertyType.Namespace)))
                continue;


            // based on the specified nest level to generate subclasses
            //    determine if we need to skip populating this property
            if (curLevel >= subClassFillLevel)
                continue;

            // This is at our best guess a custom class
            //    Call the create method to fill it
            propValue = Create(prop.PropertyType,seed, subClassFillLevel, curLevel + 1 );

            // assign that value back to the current property
            prop.SetValue(obj, propValue);
        }

        // we are done filling our properties
        //    return the populated object
        return obj;
    }


    public static dynamic? Create(Type type, int seed, int subClassFillLevel, int curLevel)
    {
        string typeName = type.Name;

        // check if we already have the reflected method info cached
        var methodInfo = GetCachedMethodInfo(nameof(CreateInternal), type);

        // If we do not have the method info already, generate it
        if (methodInfo == null)
        {
            methodInfo = typeof(FixtureLogic).GetMethod("CreateInternal");
            if (methodInfo == null)
                throw new NullReferenceException($"{nameof(CreateInternal)} method not found on class {nameof(FixtureLogic)}");

            methodInfo = methodInfo.MakeGenericMethod(type);

            // cache the method info for this type and caller so we dont waste time in reflection next time
            CacheMethodInfo(nameof(CreateInternal), type, methodInfo);
        }

        return methodInfo.Invoke(null, new object[] { seed, subClassFillLevel, curLevel });
    }



    internal static Dictionary<Type, ObjectGenerator<object>> cachedInitializers = new Dictionary<Type, ObjectGenerator<object>>();
    internal delegate T ObjectGenerator<T>(params object[] args);
    internal static ObjectGenerator<object> ObjGenExpression(Type type)
    {
        if (cachedInitializers.TryGetValue(type, out ObjectGenerator<object>? compiled))
            return compiled;

        ConstructorInfo ctor = type.GetConstructors()[0];
        ParameterInfo[] paramsInfo = ctor.GetParameters();
        ParameterExpression param = Expression.Parameter(typeof(object[]), "args");

        Expression[] argsExp = new Expression[paramsInfo.Length];

        for (int i = 0; i < paramsInfo.Length; i++)
        {
            Expression index = Expression.Constant(i);
            Type paramType = paramsInfo[i].ParameterType;

            Expression paramAccesorExp = Expression.ArrayIndex(param, index);
            Expression paramCastExp = Expression.Convert(paramAccesorExp, paramType);

            argsExp[i] = paramCastExp;
        }

        NewExpression newExp = Expression.New(ctor, argsExp);

        LambdaExpression lambda = Expression.Lambda(typeof(ObjectGenerator<object>), newExp, param);
        compiled = (ObjectGenerator<object>)lambda.Compile();

        // store the initializer for this type so we dont have to rebuild all this again
        cachedInitializers.Add(type, compiled);

        return compiled;
    }
    internal static T GenObject<T>(params object[] args)
    {
        Type type = typeof(T);

        return (T)ObjGenExpression(type).Invoke();
    }


    /// <summary>
    /// Generates a random value for the passed in Type
    /// </summary>
    /// <param name="type"></param>
    /// <param name="seed"></param>
    /// <param name="propName"></param>
    /// <param name="outValue"></param>
    /// <returns>Returns a boolean indicating if this type is a known type and was handled.</returns>
    public static bool GenValue(Type type, int currentLevel, int maxSubClassfillLevel, out dynamic? outValue, int seed = 0, string? propName = null)
    {
        // handle all of the easy basic types....
        if (type == typeof(Byte)) { outValue = new Byte_Gen(seed, propName).GetValue(); return true; }
        if (type == typeof(Int16)) { outValue = new Int16_Gen(seed, propName).GetValue(); return true; }
        if (type == typeof(Int32)) { outValue = new Int32_Gen(seed, propName).GetValue(); return true; }
        if (type == typeof(Int64)) { outValue = new Int64_Gen(seed, propName).GetValue(); return true; }
        if (type == typeof(DateOnly)) { outValue = new DateOnly_Gen(seed, propName).GetValue(); return true; }
        if (type == typeof(DateTime)) { outValue = new DateTime_Gen(seed, propName).GetValue(); return true; }
        if (type == typeof(Boolean)) { outValue = new Boolean_Gen(seed, propName).GetValue(); return true; }
        if (type == typeof(String)) { outValue = new String_Gen(seed, propName).GetValue(); return true; }
        if (type == typeof(Guid)) { outValue = new Guid_Gen(seed, propName).GetValue(); return true; }

        // now check the collection types
        if (type.Name.Contains("List")) { outValue = new List_Gen(type, seed, propName, currentLevel, maxSubClassfillLevel).GetValue(); return true; }
        if (type.Name.Contains("[]") && type.IsArray) { outValue = new Array_Gen(type, seed, propName, currentLevel, maxSubClassfillLevel).GetValue(); return true; }

        // the type is not handled and is most likely a custom class
        outValue = null;
        return false;

    }


    internal static Dictionary<string, MethodInfo> _cachedCastMethodInfos = new Dictionary<string, MethodInfo>();
    internal static MethodInfo? GetCachedMethodInfo(string callingMethod, Type type)
    {
        if (_cachedCastMethodInfos.TryGetValue($"{callingMethod}_{type.FullName}", out var methodInfo))
            return methodInfo;

        return null;
    }
    internal static void CacheMethodInfo(string callingMethod, Type type, MethodInfo methodInfo)
    {
        _cachedCastMethodInfos[$"{callingMethod}_{type.Namespace}"] = methodInfo;
    }
    public static T GenericCastToType<T>(object obj)
    {
        return (T)obj;
    }

}
