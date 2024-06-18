﻿using Containervervoer;

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
        if (container.Type == ContainerType.Cooled)
        {
            // Handle cooled containers
            var firstRow = Rows[0];
            if (firstRow.CanAddContainer(container, true))
            {
                firstRow.AddContainer(container);
                Console.WriteLine($"Added cooled container of weight {container.Weight} to the first row.");
            }
            else
            {
                throw new InvalidOperationException("Cannot add cooled container.");
            }
        }
        else if (container.Type == ContainerType.Valuable)
        {
            // Handle valuable containers
            foreach (var row in Rows)
            {
                if (row.CanAddContainer(container, Rows.IndexOf(row) == 0))
                {
                    row.AddContainer(container);
                    Console.WriteLine($"Added valuable container of weight {container.Weight}.");
                    return;
                }
            }
            throw new InvalidOperationException("Cannot add valuable container.");
        }
        else
        {
            // Handle regular containers
            foreach (var row in Rows)
            {
                if (row.CanAddContainer(container, Rows.IndexOf(row) == 0))
                {
                    row.AddContainer(container);
                    Console.WriteLine($"Added {container.Type.ToString().ToLower()} container of weight {container.Weight}.");
                    return;
                }
            }
            Console.WriteLine("No more space to add containers.");
        }
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



