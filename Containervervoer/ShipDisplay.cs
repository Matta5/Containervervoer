using Containervervoer;

public class ShipDisplay
{
    public static void Display(CargoShip ship)
    {
        // Maximaal aantal containers in een stack zodat we weten hoeveel lagen we moeten weergeven
        int maxHeight = ship.Rows.Max(row => row.Stacks.Max(stack => stack.Containers.Count));

        // Display elke layer
        for (int h = 0; h < maxHeight; h++)
        {
            Console.WriteLine($"Layer {h + 1}:");
            foreach (var row in ship.Rows)
            {
                foreach (var stack in row.Stacks)
                {
                    if (h < stack.Containers.Count)
                    {
                        // Gebruik ContainerToString om de container weer te geven
                        Console.Write($"[{ContainerToString(stack.Containers[h])}]");
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
}
