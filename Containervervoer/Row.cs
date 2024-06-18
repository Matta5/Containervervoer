﻿using Containervervoer;

public class Row
{
    private List<Stack> Stacks { get; set; }

    public Row(int width)
    {
        Stacks = new List<Stack>();
        for (int i = 0; i < width; i++)
        {
            Stacks.Add(new Stack());
        }
    }

    public void AddStack(Stack stack)
    {
        Stacks.Add(stack);
    }

    public List<Stack> GetStacks()
    {
        return new List<Stack>(Stacks);
    }



    public bool CanAddContainer(Container container, bool isFirstRow, bool isLastRow)
    {
        if (container.Type == ContainerType.Cooled && !isFirstRow)
        {
            return false;
        }

        if (container.Type == ContainerType.Valuable)
        {
            
            if (!isFirstRow && !isLastRow) return false;

            return Stacks[0].CanAddContainer(container) || Stacks[^1].CanAddContainer(container);
        }

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
}
