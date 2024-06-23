using Containervervoer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Containervervoer
{
    public class CargoShip
    {
        public int Length { get; private set; }
        public int Width { get; private set; }
        private List<Row> Rows { get; set; }

        public CargoShip(int length, int width)
        {
            Length = length;
            Width = width;
            Rows = new List<Row>();

            for (int i = 0; i < length; i++)
            {
                Rows.Add(new Row(width));
            }
        }

        public List<Row> GetRows()
        {
            return new List<Row>(Rows);
        }

        public void AddContainer(Container container)
        {
            if (container.Type == ContainerType.Cooled)
            {
                if (!TryAddCooledContainer(container))
                {
                    Console.WriteLine($"Failed to add cooled container with weight {container.Weight}.");
                    throw new InvalidOperationException("Failed to add cooled container.");
                }
                Console.WriteLine($"Cooled container with weight {container.Weight} added successfully.");
                return;
            }
            if (container.Type == ContainerType.Regular)
            {
                if (!TryAddRegularContainer(container))
                {
                    Console.WriteLine($"Failed to find a suitable place for regular container with weight {container.Weight}.");
                    throw new InvalidOperationException("No suitable place found to add the container.");
                }
                Console.WriteLine($"Regular container with weight {container.Weight} added successfully.");
                return;
            }
            if (container.Type == ContainerType.Valuable)
            {
                if (!TryAddValuableContainer(container))
                {
                    Console.WriteLine($"Failed to add valuable container with weight {container.Weight}.");
                    throw new InvalidOperationException("Failed to add valuable container.");
                }
                Console.WriteLine($"Valuable container with weight {container.Weight} added successfully.");
                return;
            }
        }




        private bool TryAddContainerBasedOnType(Container container, Func<Stack, bool> canAddToStack)
        {
            int totalStacks = this.Rows[0].PublicStacks.Count;
            int midPoint = totalStacks / 2;
            bool hasMiddleStack = totalStacks % 2 != 0;

            if (hasMiddleStack)
            {
                Stack middleStack = this.Rows[0].PublicStacks[midPoint];
                if (canAddToStack(middleStack))
                {
                    middleStack.AddContainer(container);
                    return true;
                }
            }

            bool addToLeftSide = CalculateSideToAdd(container);

            foreach (var row in this.Rows)
            {
                foreach (var stack in row.PublicStacks)
                {
                    bool isStackOnLeftSide = row.PublicStacks.IndexOf(stack) < midPoint;
                    
                    // Skip the middle stack when distributing containers to sides
                    if (hasMiddleStack && row.PublicStacks.IndexOf(stack) == midPoint) continue;

                    if (addToLeftSide == isStackOnLeftSide && canAddToStack(stack))
                    {
                        stack.AddContainer(container);
                        return true;
                    }
                }
            }

            return false;
        }


        private bool CalculateSideToAdd(Container container)
        {
            double leftSideWeight = 0;
            double rightSideWeight = 0;
            int totalStacks = this.Rows[0].PublicStacks.Count;
            int midPoint = totalStacks / 2;

            bool hasMiddleStack = totalStacks % 2 != 0;

            foreach (var row in this.Rows)
            {
                for (int i = 0; i < row.PublicStacks.Count; i++)
                {
                    if (hasMiddleStack && i == midPoint) continue; // slaat middelste container over

                    double stackWeight = row.PublicStacks[i].CalculateTotalWeight();
                    if (i < midPoint)
                    {
                        leftSideWeight += stackWeight;
                    }
                    else
                    {
                        rightSideWeight += stackWeight;
                    }
                }
            }

            return leftSideWeight <= rightSideWeight;
        }


        private bool TryAddCooledContainer(Container container)
        {
            return TryAddContainerBasedOnType(container, stack => this.Rows[0].PublicStacks.Contains(stack) && stack.CanAddContainer(container));
        }

        private bool TryAddValuableContainer(Container container)
        {
            return TryAddContainerBasedOnType(container, stack => stack.CanAddContainer(container) && stack.IsTopAccessible());
        }

        private bool TryAddRegularContainer(Container container)
        {
            return TryAddContainerBasedOnType(container, stack => stack.CanAddContainer(container));
        }


        public bool IsBalanced()
        {
            double totalWeight = this.Rows.SelectMany(row => row.PublicStacks).Sum(stack => stack.CalculateTotalWeight());
            double leftSideWeight = 0;
            double rightSideWeight = 0;
            int midPoint = this.Rows[0].PublicStacks.Count / 2;

            for (int i = 0; i < this.Rows.Count; i++)
            {
                for (int j = 0; j < this.Rows[i].PublicStacks.Count; j++)
                {
                    double stackWeight = this.Rows[i].PublicStacks[j].CalculateTotalWeight();
                    if (j < midPoint)
                    {
                        leftSideWeight += stackWeight;
                    }
                    else if (j > midPoint || this.Rows[0].PublicStacks.Count % 2 == 0) // Adjust for even number of stacks
                    {
                        rightSideWeight += stackWeight;
                    }
                }
            }

            double balanceThreshold = totalWeight * 0.2; // 20% of total weight
            return Math.Abs(leftSideWeight - rightSideWeight) <= balanceThreshold;
        }

        public bool IsAtLeastHalfFull()
        {
            double totalWeight = Rows.Sum(row => row.CalculateTotalWeight());
            double maxCapacity = Length * Width * 30;
            return totalWeight >= (maxCapacity * 0.5);
        }

    }

}

