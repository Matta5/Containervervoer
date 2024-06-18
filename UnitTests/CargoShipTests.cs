namespace Containervervoer.CargoShipTests
{
    [TestClass]
    public class CargoShipTests
    {
        [TestMethod]
        public void ShipIsExactlyHalfFull_ReturnsTrue()
        {
            var ship = new CargoShip(2, 2);
            ship.AddContainer(new Container(75, ContainerType.Regular));
            ship.AddContainer(new Container(75, ContainerType.Regular));
            ship.AddContainer(new Container(75, ContainerType.Regular));
            ship.AddContainer(new Container(75, ContainerType.Regular));

            bool result = ship.IsAtLeastHalfFull();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ShipIsMoreThanHalfFull_ReturnsTrue()
        {
            var ship = new CargoShip(2, 2);
            ship.AddContainer(new Container(80, ContainerType.Regular));
            ship.AddContainer(new Container(80, ContainerType.Regular));
            ship.AddContainer(new Container(80, ContainerType.Regular));
            ship.AddContainer(new Container(80, ContainerType.Regular));

            bool result = ship.IsAtLeastHalfFull();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ShipIsLessThanHalfFull_ReturnsFalse()
        {
            var ship = new CargoShip(2, 2);
            ship.AddContainer(new Container(70, ContainerType.Regular));
            ship.AddContainer(new Container(70, ContainerType.Regular));
            ship.AddContainer(new Container(70, ContainerType.Regular));

            bool result = ship.IsAtLeastHalfFull();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EmptyShip_ReturnsFalse()
        {
            var ship = new CargoShip(2, 2);

            bool result = ship.IsAtLeastHalfFull();

            Assert.IsFalse(result);
        }
    }
}
