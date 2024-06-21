using Containervervoer;

namespace Containervervoer
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter the length of the ship:");
            if (!int.TryParse(Console.ReadLine(), out int length))
            {
                Console.WriteLine("Invalid input for length. Please enter a valid integer.");
                return;
            }

            Console.WriteLine("Enter the width of the ship:");
            if (!int.TryParse(Console.ReadLine(), out int width))
            {
                Console.WriteLine("Invalid input for width. Please enter a valid integer.");
                return;
            }

            var ship = new CargoShip(length, width);

            var containers = new List<Container>
            {
                new Container(20, ContainerType.Valuable),
                new Container(20, ContainerType.Regular),
                new Container(15, ContainerType.Cooled),
                new Container(15, ContainerType.Cooled),
                new Container(20, ContainerType.Valuable),
                new Container(20, ContainerType.Regular),
                new Container(15, ContainerType.Cooled),
                new Container(20, ContainerType.Valuable),
                new Container(20, ContainerType.Regular),
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

            if (ship.IsBalanced())
            {
                Console.WriteLine("The ship's load is balanced or close to balanced.");
            }
            else
            {
                Console.WriteLine("The ship's load is not balanced. Consider redistributing the containers.");
            }


        }
    }
}



