using Microsoft.VisualStudio.TestTools.UnitTesting;
using Containervervoer; // Ensure this namespace matches your project structure

[TestClass]
public class CargoShipTests
{
    [TestMethod]
    public void ShipIsAtLeastHalfFull_HappyPath()
    {
        var ship = new CargoShip(2, 2);
        ship.AddContainer(new Container(100, ContainerType.Regular));
        Assert.IsTrue(ship.IsAtLeastHalfFull());
    }

    [TestMethod]
    public void ShipIsNotAtLeastHalfFull()
    {
        var ship = new CargoShip(5, 5); // Assuming maxWeight and numberOfStacks
        // Adding only one container, ship should not be half full
        ship.AddContainer(new Container(100, ContainerType.Regular));
        Assert.IsFalse(ship.IsAtLeastHalfFull());
    }

    [TestMethod]
    public void ShipBalanceWithin20Percent_HappyPath()
    {
        var ship = new CargoShip(5, 5); // Assuming maxWeight and numberOfStacks
        // Adding containers to both sides to maintain balance within 20%
        ship.AddContainer(new Container(100, ContainerType.Regular)); // Left
        ship.AddContainer(new Container(100, ContainerType.Regular)); // Right
        Assert.IsTrue(ship.IsBalanced());
    }

    [TestMethod]
    public void ShipBalanceExceeds20Percent()
    {
        var ship = new CargoShip(5, 5); // Assuming maxWeight and numberOfStacks
        // Adding more weight to one side to exceed the 20% balance threshold
        ship.AddContainer(new Container(300, ContainerType.Regular)); // Left
        ship.AddContainer(new Container(100, ContainerType.Regular)); // Right
        Assert.IsFalse(ship.IsBalanced());
    }
}