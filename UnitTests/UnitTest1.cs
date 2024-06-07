using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Containervervoer;

[TestClass]
public class Tests
{
    [TestMethod]
    public void TestMaxWeightPerContainer()
    {
        // Implement test logic
    }

    [TestMethod]
    public void TestValuableContainersAccessible()
    {
        // Implement test logic
    }

    [TestMethod]
    public void TestRefrigeratedContainersInFirstRow()
    {
        // Implement test logic
    }

    [TestMethod]
    public void TestMinimumWeightUsed()
    {
        var ship = new Ship(200, 4, 4);
        var container = new Containers(100);
        ship.AddContainer(container, new Position(0, 0));
        Assert.IsTrue(ship.IsMinimumWeightUsed());
    }

    [TestMethod]
    public void TestBalanced()
    {
        var ship = new Ship(200, 4, 4);
        var container1 = new Containers(50);
        var container2 = new Containers(50);
        ship.AddContainer(container1, new Position(0, 0));
        ship.AddContainer(container2, new Position(0, 3));
        Assert.IsTrue(ship.IsBalanced());
    }
}
