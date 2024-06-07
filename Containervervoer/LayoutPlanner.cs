 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containervervoer
{
    public class LayoutPlanner
    {
        public void GenerateLayout(Ship ship, List<Containers> containers)
        {
            // Add logic to generate a valid layout
        }

        public void VisualizeLayout(Ship ship)
        {
            for (int i = 0; i < ship.Length; i++)
            {
                for (int j = 0; j < ship.Width; j++)
                {
                    if (ship.Containers[i, j] == null)
                    {
                        Console.Write("[ ]");
                    }
                    else
                    {
                        Console.Write("[C]");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
