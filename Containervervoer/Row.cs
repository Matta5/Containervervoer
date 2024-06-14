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

    public bool CanAddContainer(Container container, bool isFirstRow)
    {
        // Alle cooled containers moeten op de eerste rij worden geplaatst
        if (container.Type == ContainerType.Cooled && !isFirstRow)
        {
            return false;
        }

        // kijk of er een stack is waar de valuable container op kan
        if (container.Type == ContainerType.Valuable)
        {
            // Check of er een stack is waar de valuable container op kan
            return Stacks.Any(stack => !stack.Containers.Any() || (stack.Containers.Count == 1 && stack.Containers[0].Type != ContainerType.Valuable));
        }

        // Kijk of er een stack is waar de container op kan
        return Stacks.Any(stack => stack.CanAddContainer(container));
    }

    public void AddContainer(Container container)
    {
        if (container.Type == ContainerType.Cooled)
        {
            // probeer cooled container toe te voegen aan de eerste stack
            if (Stacks[0].CanAddContainer(container))
            {
                Stacks[0].AddContainer(container);
            }
            else
            {
                throw new InvalidOperationException("Cannot add cooled container to the first stack.");
            }
        }
        else
        {
            // Voeg container toe aan de eerste stack waar het kan
            foreach (var stack in Stacks)
            {
                if (stack.CanAddContainer(container))
                {
                    stack.AddContainer(container);
                    return;
                }
            }

            // als er geen stack is waar de container op kan, gooi een exception
            throw new InvalidOperationException("Cannot add container to any stack.");
        }
    }
}