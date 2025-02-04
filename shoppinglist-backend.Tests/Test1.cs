namespace shoppinglist_backend.Tests;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void TestTableName()
    {
        StaticConfiguration.Name = "Per";

        var result = TableStorageService.GetTableName();

        Assert.AreEqual("PerSinHandleliste", result);

        StaticConfiguration.Name = "Pippi";

        result = TableStorageService.GetTableName();

        Assert.AreEqual("PippiSinHandleliste", result);
    }
}
