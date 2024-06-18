namespace Containervervoer.CargoShipTests
{
    [TestClass]
    public class CargoShipTests
    {
        [TestMethod]
        public void ShipIsExactlyHalfFull_ReturnsTrue()
        {
            var ship = new CargoShip(2, 2);
            // Assuming each container weighs 30 and each stack can hold up to 150.
            // To be exactly 50% full, the ship needs to carry 300 in weight.
            ship.AddContainer(new Container(150, ContainerType.Regular));
            ship.AddContainer(new Container(150, ContainerType.Regular));

            bool result = ship.IsAtLeastHalfFull();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ShipIsMoreThanHalfFull_ReturnsTrue()
        {
            var ship = new CargoShip(2, 2);
            // Adding more than 300 in weight to ensure it's more than 50% full.
            ship.AddContainer(new Container(150, ContainerType.Regular));
            ship.AddContainer(new Container(150, ContainerType.Regular));
            ship.AddContainer(new Container(30, ContainerType.Regular));

            bool result = ship.IsAtLeastHalfFull();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ShipIsLessThanHalfFull_ReturnsFalse()
        {
            var ship = new CargoShip(2, 2);
            ship.AddContainer(new Container(150, ContainerType.Regular));

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
