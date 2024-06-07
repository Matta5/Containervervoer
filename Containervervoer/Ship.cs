using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containervervoer
{
    public class Ship
    {
        public double MaxWeight { get; private set; }
        public double CurrentWeight { get; private set; }
        public int Length { get; private set; }
        public int Width { get; private set; }
        private Containers[,] Containers;

        public Ship(double maxWeight, int length, int width)
        {
            MaxWeight = maxWeight;
            Length = length;
            Width = width;
            Containers = new Containers[length, width];
            CurrentWeight = 0;
        }

        public void AddContainer(Containers container, Position position)
        {
            if (Containers[position.Row, position.Column] == null)
            {
                Containers[position.Row, position.Column] = container;
                CurrentWeight += container.Weight;
            }
        }

        public void RemoveContainer(Position position)
        {
            var container = Containers[position.Row, position.Column];
            if (container != null)
            {
                CurrentWeight -= container.Weight;
                Containers[position.Row, position.Column] = null;
            }
        }

        public double CalculateCurrentWeight()
        {
            return CurrentWeight;
        }

        public bool IsBalanced()
        {
            double leftWeight = 0, rightWeight = 0;
            int mid = Width / 2;

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < mid; j++)
                {
                    if (Containers[i, j] != null)
                    {
                        leftWeight += Containers[i, j].Weight;
                    }
                }
                for (int j = mid; j < Width; j++)
                {
                    if (Containers[i, j] != null)
                    {
                        rightWeight += Containers[i, j].Weight;
                    }
                }
            }

            return Math.Abs(leftWeight - rightWeight) <= 0.2 * CurrentWeight;
        }

        public bool IsMinimumWeightUsed()
        {
            return CurrentWeight >= 0.5 * MaxWeight;
        }

        public bool IsValid()
        {
            // Add validation logic for other constraints
            return IsBalanced() && IsMinimumWeightUsed();
        }
    }
}
