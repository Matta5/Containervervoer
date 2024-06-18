namespace Containervervoer
{
    public class Stack
    {
        private List<Container> Containers { get; set; }
        public IReadOnlyList<Container> containers { get { return Containers; } }

        public Stack()
        {
            Containers = new List<Container>();
        }

        public IReadOnlyList<Container> GetContainers()
        {
            return Containers;
        }

        public bool CanAddContainer(Container container)
        {
            if (Containers.Skip(1).Sum(c => c.Weight) + container.Weight > 120)
            {
                return false;
            }

            if (Containers.Any() && Containers.Last().Type == ContainerType.Valuable)
            {
                return false;
            }

            return true;
        }

        public void AddContainer(Container container)
        {
            if (!CanAddContainer(container))
            {
                throw new InvalidOperationException("Kan container niet aan de stapel toevoegen.");
            }

            Containers.Add(container); // Voeg de container toe aan de lijst
        }
    }
}


