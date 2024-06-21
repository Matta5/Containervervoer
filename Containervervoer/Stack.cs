using System.Collections.Generic;
using System.Linq;

namespace Containervervoer
{
    public class Stack
    {
        private const double MaxWeightOnTop = 120;
        private List<Container> Containers { get; set; } = new List<Container>();

        public bool IsTopAccessible()
        {
            if (Containers.Count == 0) return true; 
            return Containers.Last().Type != ContainerType.Valuable;
        }

        public bool CanAddContainer(Container newContainer)
        {
            if (!Containers.Any() || newContainer.Type == ContainerType.Valuable)
            {
                return true;
            }

            var topContainer = Containers.Last();
            if (topContainer.Type == ContainerType.Valuable)
            {
                return false;
            }

            if (!CanSupportMoreWeight(newContainer.Weight))
            {
                return false;
            }

            return true;
        }

        public bool CanSupportMoreWeight(double additionalWeight)
        {
            const double maxWeightOnTop = 120;
            double currentWeight = Containers.Sum(container => container.Weight);
            return (currentWeight + additionalWeight) <= maxWeightOnTop;
        }

        public void AddContainer(Container container)
        {
            if (CanAddContainer(container))
            {
                Containers.Add(container);
            }
            else
            {
                throw new InvalidOperationException("Cannot add container to the stack.");
            }
        }

        public double CalculateTotalWeight()
        {
            return Containers.Sum(container => container.Weight);
        }

        public List<Container> GetContainers()
        {
            return new List<Container>(Containers);
        }
    }

}
