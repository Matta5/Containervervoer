namespace Containervervoer
{
    public class Stack
    {
        public List<Container> Containers { get; private set; }

        public Stack()
        {
            Containers = new List<Container>(); // Initialiseer een nieuwe lijst van containers
        }

        public bool CanAddContainer(Container container)
        {
            // Controleer of er iets op een waardevolle container kan worden gestapeld
            if (Containers.Any() && container.Type == ContainerType.Valuable)
            {
                return false; // Waardevolle containers kunnen niet op een andere container worden geplaatst
            }

            // Het maximale gewicht bovenop een container is 120 ton
            if (Containers.Sum(c => c.Weight) + container.Weight > 120)
            {
                return false;
            }

            // Zorg ervoor dat waardevolle containers op een toegankelijke manier worden geplaatst
            if (container.Type == ContainerType.Valuable && Containers.Any(c => c.Type == ContainerType.Valuable))
            {
                return false; // Voorkom dat er iets op een waardevolle container wordt gestapeld
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