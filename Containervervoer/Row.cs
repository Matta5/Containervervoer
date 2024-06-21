using System.Collections.Generic;
using System.Linq;

namespace Containervervoer
{
    public class Row
    {
        private List<Stack> Stacks { get; set; }

        // Public property to access Stacks
        public List<Stack> PublicStacks
        {
            get { return Stacks; }
        }

        public Row(int width)
        {
            Stacks = new List<Stack>();
            for (int i = 0; i < width; i++)
            {
                Stacks.Add(new Stack());
            }
        }
        public int RowIndex { get; private set; }


        public bool CanAddContainer(Container container, bool addToLeftSide, int shipLength)
        {
            var eligibleStacks = GetEligibleStacks(addToLeftSide);

            foreach (var stack in eligibleStacks)
            {
                if (container.Type == ContainerType.Cooled && RowIndex != 0)
                {
                    // Cooled containers can only be added in the first row
                    continue;
                }

                if (container.Type == ContainerType.Valuable && !stack.IsTopAccessible())
                {
                    // Valuable containers must be accessible, i.e., cannot have containers stacked on top
                    continue;
                }

                if (stack.CanAddContainer(container))
                {
                    // Check if adding this container exceeds the maximum allowed weight on top of the stack
                    continue;
                }

                // If all conditions are met, this stack can accept the container
                return true;
            }

            // No eligible stack found that can accept the container under the given conditions
            return false;
        }

        public void AddContainer(Container container, bool addToLeftSide)
            {
                var eligibleStacks = GetEligibleStacks(addToLeftSide);
                foreach (var stack in eligibleStacks)
                {
                    if (stack.CanAddContainer(container))
                    {
                        stack.AddContainer(container);
                        return;
                    }
                }
                throw new InvalidOperationException("No suitable stack found to add the container.");
            }

            private IEnumerable<Stack> GetEligibleStacks(bool addToLeftSide)
            {
                int halfStacks = Stacks.Count / 2;
                return addToLeftSide ? Stacks.Take(halfStacks) : Stacks.Skip(halfStacks);
            }
        


        public double CalculateTotalWeightLeftSide()
        {
            int halfStacks = Stacks.Count / 2;
            return Stacks.Take(halfStacks).Sum(stack => stack.CalculateTotalWeight());
        }

        public double CalculateTotalWeightRightSide()
        {
            int halfStacks = Stacks.Count / 2;
            return Stacks.Skip(halfStacks).Sum(stack => stack.CalculateTotalWeight());
        }

        public double CalculateTotalWeight()
        {
            return Stacks.Sum(stack => stack.CalculateTotalWeight());
        }

        public List<Stack> GetStacks()
        {
            return new List<Stack>(Stacks);
        }
    }

}
