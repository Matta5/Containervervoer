using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containervervoer
{
    public class Containers
    {
        public double Weight { get; private set; }
        public bool IsValuable { get; private set; }
        public bool IsRefrigerated { get; private set; }

        public Containers(double weight, bool isValuable = false, bool isRefrigerated = false)
        {
            Weight = weight;
            IsValuable = isValuable;
            IsRefrigerated = isRefrigerated;
        }
    }
}
