using Containervervoer;

public class Row
{
    public List<Stack> Stacks { get; private set; }

    public Row(int width)
    {
        Stacks = new List<Stack>();
        for (int i = 0; i < width; i++)
        {
            Stacks.Add(new Stack());
        }
    }

    public List<Stack> GetStacks()
    {
        return new List<Stack>(Stacks);
    }

    public bool CanAddContainer(Container container, int rowIndex, List<Row> allRows)
    {
        // Enforce cooled containers to be in the first row only
        if (container.Type == ContainerType.Cooled && rowIndex != 0)
        {
            return false;
        }

         if (container.Type == ContainerType.Valuable)
        {
            // Valuable containers can be placed in any row but need an open door.
            bool isFirstLayer = Stacks.All(s => s.GetContainers().Count == 0);
            bool hasOpenDoor = isFirstLayer || rowIndex == 0 || rowIndex == allRows.Count - 1;

            // Check for staircase condition if not the first or last row
            if (!hasOpenDoor && rowIndex > 0 && rowIndex < allRows.Count - 1)
            {
                var prevRowHeight = allRows[rowIndex - 1].GetMaxHeight();
                var nextRowHeight = allRows[rowIndex + 1].GetMaxHeight();
                var currentRowHeight = GetMaxHeight();

                hasOpenDoor = prevRowHeight < currentRowHeight || nextRowHeight < currentRowHeight;
            }

            return hasOpenDoor && Stacks.Any(s => s.CanAddContainer(container));
        }

        // General logic for adding containers (applies to regular containers)
        return Stacks.Any(stack => stack.CanAddContainer(container));
    }

    public void AddContainer(Container container)
    {
        if (container.Type == ContainerType.Cooled)
        {
            bool added = false;
            foreach (var stack in Stacks)
            {
                if (stack.CanAddContainer(container))
                {
                    stack.AddContainer(container);
                    added = true;
                    break;
                }
            }

            if (!added)
            {
                throw new InvalidOperationException("Cannot add cooled container to the first row.");
            }
        }
        else
        {
            foreach (var stack in Stacks)
            {
                if (stack.CanAddContainer(container))
                {
                    stack.AddContainer(container);
                    return;
                }
            }

            throw new InvalidOperationException("Cannot add container to any stack.");
        }
    }

    public int GetMaxHeight()
    {
        return Stacks.Max(stack => stack.GetContainers().Count);
    }
}