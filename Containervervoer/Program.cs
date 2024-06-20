using Containervervoer;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the length of the ship:");
        int length = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the width of the ship:");
        int width = int.Parse(Console.ReadLine());

        var ship = new CargoShip(length, width);

        var containers = new List<Container>
            {
                new Container(10, ContainerType.Regular),
                new Container(20, ContainerType.Valuable),
                new Container(10, ContainerType.Valuable),
                new Container(10, ContainerType.Valuable),
                new Container(15, ContainerType.Cooled),
                new Container(5, ContainerType.Regular),
                new Container(30, ContainerType.Regular),new Container(30, ContainerType.Regular),new Container(30, ContainerType.Regular),new Container(30, ContainerType.Regular),
                new Container(12, ContainerType.Valuable),
                new Container(18, ContainerType.Cooled)
            };

        var sortedContainers = containers
            .OrderByDescending(c => c.Type == ContainerType.Cooled)
            .ThenByDescending(c => c.Type == ContainerType.Regular)
            .ThenByDescending(c => c.Weight)
            .ToList();

        
        foreach (var container in sortedContainers.Where(c => c.Type != ContainerType.Valuable))
        {
            ship.AddContainer(container);
        }

        foreach (var container in sortedContainers.Where(c => c.Type == ContainerType.Valuable))
        {
            ship.AddContainer(container);
        }

        ShipDisplay.Display(ship);

        ShipDisplay.DisplayIfHalfOfMaxWeight(ship);
    }
}