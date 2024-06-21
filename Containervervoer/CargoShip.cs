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
            // Handle valuable containers with accessibility in mind
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




        private bool TryAddCooledContainer(Container container)
        {
            Row firstRow = this.Rows[0];
            // Calculate total weight on each side
            double leftSideWeight = 0;
            double rightSideWeight = 0;

            int midPoint = firstRow.PublicStacks.Count / 2;
            for (int i = 0; i < firstRow.PublicStacks.Count; i++)
            {
                double stackWeight = firstRow.PublicStacks[i].CalculateTotalWeight();
                if (i < midPoint)
                {
                    leftSideWeight += stackWeight;
                }
                else
                {
                    rightSideWeight += stackWeight;
                }
            }

            bool addToLeftSide = leftSideWeight <= rightSideWeight;

            foreach (var stack in firstRow.PublicStacks)
            {
                bool isStackOnLeftSide = firstRow.PublicStacks.IndexOf(stack) < midPoint;
                if (addToLeftSide == isStackOnLeftSide && stack.CanAddContainer(container))
                {
                    stack.AddContainer(container);
                    return true;
                }
            }

            return false;
        }


        private bool TryAddValuableContainer(Container container)
        {
            double leftSideWeight = 0;
            double rightSideWeight = 0;
            int midPoint = this.Rows[0].PublicStacks.Count / 2; 

            foreach (var row in this.Rows)
            {
                for (int i = 0; i < row.PublicStacks.Count; i++)
                {
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

            bool addToLeftSide = leftSideWeight <= rightSideWeight;

            foreach (var row in this.Rows)
            {
                foreach (var stack in row.PublicStacks)
                {
                    bool isStackOnLeftSide = row.PublicStacks.IndexOf(stack) < midPoint;
                    if (addToLeftSide == isStackOnLeftSide && stack.CanAddContainer(container) && stack.IsTopAccessible())
                    {
                        stack.AddContainer(container);
                        return true;
                    }
                }
            }

            return false;
        }


        private bool TryAddRegularContainer(Container container)
        {
            double leftSideWeight = 0;
            double rightSideWeight = 0;
            int midPoint = this.Rows[0].PublicStacks.Count / 2;

            foreach (var row in this.Rows)
            {
                for (int i = 0; i < row.PublicStacks.Count; i++)
                {
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

            bool addToLeftSide = leftSideWeight <= rightSideWeight;

            foreach (var row in this.Rows)
            {
                foreach (var stack in row.PublicStacks)
                {
                    bool isStackOnLeftSide = row.PublicStacks.IndexOf(stack) < midPoint;
                    if (addToLeftSide == isStackOnLeftSide && stack.CanAddContainer(container))
                    {
                        stack.AddContainer(container);
                        return true;
                    }
                }
            }

            return false;
        }


        public bool IsBalanced()
        {
            double leftWeight = 0;
            double rightWeight = 0;

            foreach (var row in Rows)
            {
                for (int i = 0; i < row.GetStacks().Count; i++)
                {
                    if (i < row.GetStacks().Count / 2) leftWeight += row.GetStacks()[i].CalculateTotalWeight();
                    else rightWeight += row.GetStacks()[i].CalculateTotalWeight();
                }
            }

            double totalWeight = leftWeight + rightWeight;
            return Math.Abs(leftWeight - rightWeight) <= totalWeight * 0.2;
        }

        public bool IsAtLeastHalfFull()
        {
            double totalWeight = Rows.Sum(row => row.CalculateTotalWeight());
            double maxCapacity = Length * Width * 30;
            return totalWeight >= (maxCapacity * 0.5);
        }

    }

}

