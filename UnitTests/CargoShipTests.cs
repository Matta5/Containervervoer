using Containervervoer;

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
        var ship = new CargoShip(4, 4); 
        ship.AddContainer(new Container(100, ContainerType.Regular));
        Assert.IsFalse(ship.IsAtLeastHalfFull());
    }

    [TestMethod]
    public void ShipBalanceWithin20Percent_HappyPath()
    {
        var ship = new CargoShip(4, 4);
        ship.AddContainer(new Container(100, ContainerType.Regular)); 
        ship.AddContainer(new Container(100, ContainerType.Regular)); 
        Assert.IsTrue(ship.IsBalanced());
    }

    [TestMethod]
    public void ShipBalanceExceeds20Percent()
    {
        var ship = new CargoShip(4, 4);
        ship.AddContainer(new Container(300, ContainerType.Regular)); 
        ship.AddContainer(new Container(100, ContainerType.Regular)); 
        Assert.IsFalse(ship.IsBalanced());
    }
}