using Containervervoer;

public class ShipDisplay
{
    public static void Display(CargoShip ship)
    {
        int maxHeight = ship.GetRows().Max(row => row.GetStacks().Max(stack => stack.GetContainers().Count));

        for (int h = 0; h < maxHeight; h++)
        {
            Console.WriteLine($"Layer {h + 1}:");
            foreach (var row in ship.GetRows())
            {
                foreach (var stack in row.GetStacks())
                {
                    if (h < stack.GetContainers().Count)
                    {
                        Console.Write($"[{ContainerToString(stack.GetContainers()[h])}]");
                    }
                    else
                    {
                        Console.Write("[ ]");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    public static string ContainerToString(Container container)
    {
        switch (container.Type)
        {
            case ContainerType.Valuable:
                return "V";
            case ContainerType.Cooled:
                return "C";
            default:
                return "R";
        }
    }

    public static void DisplayIfHalfOfMaxWeight(CargoShip ship)
    {
        if (ship.IsAtLeastHalfFull())
        {
            Console.WriteLine("The ship is properly loaded.");
        }
        else
        {
            Console.WriteLine("The ship is not loaded enough. It must be at least 50% of its maximum capacity.");
        }
    }
}
