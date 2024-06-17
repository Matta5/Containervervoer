namespace Containervervoer
{
    public class Stack
    {
        public List<Container> Containers { get; private set; }

        public Stack()
        {
            Containers = new List<Container>();
        }

        public bool CanAddContainer(Container container)
        {
            // Het maximale gewicht bovenop een container is 120 ton
            if (Containers.Sum(c => c.Weight) + container.Weight > 120)
            {
                return false;
            }

            // Als de stapel al containers bevat, controleer dan of de bovenste container waardevol is
            if (Containers.Any() && Containers.Last().Type == ContainerType.Valuable)
            {
                return false; // Voorkom dat er op een waardevolle container wordt gestapeld
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