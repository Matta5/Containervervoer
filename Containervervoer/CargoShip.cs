using Containervervoer;

public class CargoShip
{
    public int Length { get; private set; }
    public int Width { get; private set; }
    public List<Container>[,] Grid { get; private set; }

    public CargoShip(int length, int width)
    {
        Length = length;
        Width = width;
        Grid = new List<Container>[length, width];
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Grid[i, j] = new List<Container>();
            }
        }
    }

    public void AddContainer(Container container)
    {
        int minHeight = int.MaxValue;
        int targetRow = -1;
        int targetCol = -1;

        // Find the position with the minimum height
        for (int i = 0; i < Length; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (Grid[i, j].Count < minHeight)
                {
                    minHeight = Grid[i, j].Count;
                    targetRow = i;
                    targetCol = j;
                }
            }
        }

        // Check if a suitable position is found
        if (targetRow != -1 && targetCol != -1)
        {
            Grid[targetRow, targetCol].Add(container);
        }
        else
        {
            Console.WriteLine("No more space to add containers.");
        }
    }


    public void Display()
    {
        // Determine the maximum height dynamically
        int maxHeight = 0;
        for (int i = 0; i < Length; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (Grid[i, j].Count > maxHeight)
                {
                    maxHeight = Grid[i, j].Count;
                }
            }
        }

        // Display each layer
        for (int h = 0; h < maxHeight; h++)
        {
            Console.WriteLine($"Layer {h + 1}:");
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (h < Grid[i, j].Count)
                    {
                        Console.Write($"[{Grid[i, j][h]}]");
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
}
