using System.Collections.Generic;
using System.Linq;

namespace Containervervoer
{
    public class Row
    {
        private List<Stack> Stacks { get; set; }

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
