using System.Text;

namespace RepeatFixture.ValueGenerators;

internal class List_Gen : ICollectionValueGenerator
{
    public List_Gen(Type type, int seed, string? propName, int currentLevel, int maxSubClassfillLevel) : base(type, seed, propName, currentLevel, maxSubClassfillLevel) { }

    public override dynamic GetValue()
    {
        // determine the type of items stored in this list
        Type recordType = _type.GenericTypeArguments[0].UnderlyingSystemType;

        // create a new instance of the List
        dynamic? objList = Activator.CreateInstance(typeof(List<>).MakeGenericType(recordType));
        if (objList == null)
            return new List<dynamic>();

        // if this is a known base type, generate a simple value for it and return the filled array
        if (FixtureLogic.GenValue(recordType, _currentLevel, _maxSubClassFillLevel, out var itemValue, _seed, _propName))
        {
            objList.Add(itemValue);
            return objList;
        }

        if (_currentLevel > _maxSubClassFillLevel)
            return objList;

        // The type is not a simple one, we need to dynamically create it and add it to the list
        var listItem = FixtureLogic.Create(recordType, _seed + 1, _maxSubClassFillLevel, _currentLevel);
        if (listItem == null) 
            return new List<dynamic>();

        objList.Add(CastToType(recordType, listItem));
        return objList;
    }
}
