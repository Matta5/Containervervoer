using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containervervoer
{
    public class Row
    {
        public List<Stack> Stacks { get; private set; }

        public bool CanAddContainer(Container container, bool isFirstRow)
        {
            // All coolable containers must be in the first row
            if (container.Type == ContainerType.Cooled && !isFirstRow)
            {
                return false;
            }

            // Valuable containers must be accessible from the front or back
            if (container.Type == ContainerType.Valuable && Stacks.Count > 1)
            {
                return false;
            }

            return Stacks.Any(stack => stack.CanAddContainer(container));
        }

        public void AddContainer(Container container)
        {
            foreach (var stack in Stacks)
            {
                if (stack.CanAddContainer(container))
                {
                    stack.AddContainer(container);
                    break;
                }
            }
        }
    }
}
