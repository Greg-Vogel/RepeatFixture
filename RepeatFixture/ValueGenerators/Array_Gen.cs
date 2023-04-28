namespace RepeatFixture.ValueGenerators;

internal class Array_Gen : ICollectionValueGenerator
{
    public Array_Gen(Type type, int seed, string? propName, int currentLevel, int maxSubClassfillLevel) : base(type, seed, propName, currentLevel, maxSubClassfillLevel) { }

    public override dynamic GetValue()
    {
        // determine the type of items stored in this list
        Type? recordType = _type.GetElementType();

        if (recordType == null)
            throw new NullReferenceException($"ElementType could not be found for PropertyName: {_propName} on Type: {_type.Name}");

        // create a new instance of the List
        dynamic? objList = Activator.CreateInstance(typeof(List<>).MakeGenericType(recordType));
        if (objList == null)
            return new List<dynamic>().ToArray();

        // if this is a known base type, generate a simple value for it and return the filled array
        if (FixtureLogic.GenValue(recordType, _currentLevel, _maxSubClassFillLevel, out var itemValue, _seed, _propName))
        {
            objList.Add(CastToType(recordType, itemValue));
            return objList.ToArray();
        }

        if (_currentLevel > _maxSubClassFillLevel)
            return objList;

        // The type is not simple and needs to be dynamically created
        //    create that item now and then return it
        var listItem = FixtureLogic.Create(recordType, _seed + 1, _maxSubClassFillLevel, _currentLevel);
        if (listItem == null)
            return new List<dynamic>().ToArray();

        objList.Add(CastToType(recordType, listItem));
        return objList.ToArray();
    }
}
