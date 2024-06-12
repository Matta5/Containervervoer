using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containervervoer
{
    public class Stack
    {
        public List<Container> Containers { get; private set; }

        public bool CanAddContainer(Container container)
        {
            // Nothing can be stacked on top of a valuable container
            if (Containers.Any(c => c.Type == ContainerType.Valuable))
            {
                return false;
            }

            // The maximum weight on top of a container is 120 tons
            if (Containers.Sum(c => c.Weight) + container.Weight > 120)
            {
                return false;
            }

            return true;
        }

        public void AddContainer(Container container)
        {
            if (!CanAddContainer(container))
            {
                throw new InvalidOperationException("Cannot add container to the stack.");
            }

            Containers.Add(container);
        }
    }
}
