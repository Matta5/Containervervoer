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
            new Container(15, ContainerType.Cooled),
            new Container(5, ContainerType.Regular),
            new Container(8, ContainerType.Regular),
            new Container(12, ContainerType.Valuable),
            new Container(18, ContainerType.Cooled)
        };

        foreach (var container in containers)
        {
            ship.AddContainer(container);
        }

        ship.Display();
    }
}