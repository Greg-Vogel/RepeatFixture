using Tests.Classes;

namespace Tests.Tests;

[TestClass]
public class MultiLayerClassTests : VerifyBase
{
    [TestMethod]
    public Task Create_TestTwoLayerClass_Default()
    {
        var expected = RepeatFixture.RepeatFixture.Create<Department>();

        return Verify(expected);
    }

    [TestMethod]
    public Task Create_TestTwoLayerClass_TopLayer()
    {
        var expected = RepeatFixture.RepeatFixture.Create<Department>(1);

        return Verify(expected);
    }

    [TestMethod]
    public Task Create_TestTwoLayerClass_TopPlus1()
    {
        var expected = RepeatFixture.RepeatFixture.Create<Department>(2);

        return Verify(expected);
    }


    [TestMethod]
    public Task CreateMany_TestTwoLayerClass_Default()
    {
        var expected = RepeatFixture.RepeatFixture.CreateMany<Department>();

        return Verify(expected);
    }

    [TestMethod]
    public Task CreateMany_TestTwoLayerClass_TopLayer()
    {
        var expected = RepeatFixture.RepeatFixture.CreateMany<Department>(1);

        return Verify(expected);
    }

    [TestMethod]
    public Task CreateMany_TestTwoLayerClass_TopPlus1()
    {
        var expected = RepeatFixture.RepeatFixture.CreateMany<Department>(2);

        return Verify(expected);
    }




    [TestMethod]
    public Task Create_TestThreeLayerClass_Default()
    {
        var expected = RepeatFixture.RepeatFixture.Create<Company>();

        return Verify(expected);
    }

    [TestMethod]
    public Task Create_TestThreeLayerClass_TopLayer()
    {
        var expected = RepeatFixture.RepeatFixture.Create<Company>(1);

        return Verify(expected);
    }

    [TestMethod]
    public Task Create_TestThreeLayerClass_TopPlus1()
    {
        var expected = RepeatFixture.RepeatFixture.Create<Company>(2);

        return Verify(expected);
    }
    [TestMethod]
    public Task Create_TestThreeLayerClass_TopPlus2()
    {
        var expected = RepeatFixture.RepeatFixture.Create<Company>(3);

        return Verify(expected);
    }


    [TestMethod]
    public Task CreateMany_TestThreeLayerClass_Default()
    {
        var expected = RepeatFixture.RepeatFixture.CreateMany<Company>();

        return Verify(expected);
    }

    [TestMethod]
    public Task CreateMany_TestThreeLayerClass_TopLayer()
    {
        var expected = RepeatFixture.RepeatFixture.CreateMany<Company>(1);

        return Verify(expected);
    }

    [TestMethod]
    public Task CreateMany_TestThreeLayerClass_TopPlus1()
    {
        var expected = RepeatFixture.RepeatFixture.CreateMany<Company>(2);

        return Verify(expected);
    }
    [TestMethod]
    public Task CreateMany_TestThreeLayerClass_TopPlus2()
    {
        var expected = RepeatFixture.RepeatFixture.CreateMany<Company>(3);

        return Verify(expected);
    }
}