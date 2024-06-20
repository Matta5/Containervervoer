using Containervervoer;

public class CargoShip
{
    public int Length { get; private set; }
    public int Width { get; private set; }
    private List<Row> Rows { get; set; }
    
    public List<Row> GetRows()
    {
        return new List<Row>(Rows);
    }

    public CargoShip(int length, int width)
    {
        Length = length;
        Width = width;
        Rows = new List<Row>();

        for (int i = 0; i < length; i++)
        {
            Rows.Add(new Row(width));
        }
    }

    public void AddContainer(Container container)
    {
        switch (container.Type)
        {
            case ContainerType.Cooled:
                AddCooledContainer(container);
                break;
            case ContainerType.Regular:
                AddRegularContainer(container);
                break;
            case ContainerType.Valuable:
                AddValuableContainer(container);
                break;            
            default:
                throw new InvalidOperationException("Unknown container type.");
        }
    }

    private void AddCooledContainer(Container container)
    {
        var firstRow = Rows[0];
        if (!firstRow.CanAddContainer(container, 0, Rows))
        {
            throw new InvalidOperationException("Cannot add cooled container.");
        }
        firstRow.AddContainer(container);
        Console.WriteLine($"Added cooled container of weight {container.Weight} to the first row.");
    }

    private void AddValuableContainer(Container container)
    {
        for (int rowIndex = 0; rowIndex < Rows.Count; rowIndex++)
        {
            var row = Rows[rowIndex];
            if (row.CanAddContainer(container, rowIndex, Rows))
            {
                row.AddContainer(container);
                Console.WriteLine($"Added valuable container of weight {container.Weight}.");
                return;
            }
        }
        throw new InvalidOperationException("Cannot add valuable container.");
    }

    private void AddRegularContainer(Container container)
    {
        for (int rowIndex = 0; rowIndex < Rows.Count; rowIndex++)
        {
            var row = Rows[rowIndex];
            // Now passing the required arguments to CanAddContainer
            if (row.CanAddContainer(container, rowIndex, Rows))
            {
                row.AddContainer(container);
                Console.WriteLine($"Added regular container of weight {container.Weight}.");
                return;
            }
        }
        Console.WriteLine("No more space to add regular containers.");
    }




    public double CalculateTotalWeight()
    {
        double totalWeight = 0;
        foreach (var row in GetRows())
        {
            foreach (var stack in row.GetStacks())
            {
                totalWeight += stack.GetContainers().Sum(container => container.Weight);
            }
        }
        return totalWeight;
    }

    public bool IsAtLeastHalfFull()
    {
        double totalWeight = CalculateTotalWeight();
        double maxCapacity = Length * Width * 150;
        return totalWeight >= (maxCapacity * 0.5);
    }




}



