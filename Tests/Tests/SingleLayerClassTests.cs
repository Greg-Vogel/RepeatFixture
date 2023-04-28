using Tests.Classes;

namespace Tests.Tests;

[TestClass]
public class SingleLayerClassTests : VerifyBase
{
    [TestMethod]
    public Task Create_TestSingleLayerClass_Default()
    {
        var expected = RepeatFixture.RepeatFixture.Create<Member>();

        return Verify(expected);
    }

    [TestMethod]
    public Task Create_TestSingleLayerClass_TopLayer()
    {
        var expected = RepeatFixture.RepeatFixture.Create<Member>(1);

        return Verify(expected);
    }

    [TestMethod]
    public Task Create_TestSingleLayerClass_TopPlus1()
    {
        var expected = RepeatFixture.RepeatFixture.Create<Member>(2);

        return Verify(expected);
    }


    [TestMethod]
    public Task CreateMany_TestSingleLayerClass_Default()
    {
        var expected = RepeatFixture.RepeatFixture.CreateMany<Member>();

        return Verify(expected);
    }

    [TestMethod]
    public Task CreateMany_TestSingleLayerClass_TopLayer()
    {
        var expected = RepeatFixture.RepeatFixture.CreateMany<Member>(1);

        return Verify(expected);
    }

    [TestMethod]
    public Task CreateMany_TestSingleLayerClass_TopPlus1()
    {
        var expected = RepeatFixture.RepeatFixture.CreateMany<Member>(2);

        return Verify(expected);
    }
}