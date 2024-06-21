using Containervervoer;

namespace Containervervoer
{
    public enum ContainerType
    {
        Regular,
        Valuable,
        Cooled
    }

    public class Container
    {
        public ContainerType Type { get; set; }
        public double Weight { get; set; } // Weight in tons

        public Container(double weight, ContainerType type)
        {
            Type = type;
            Weight = weight;
        }
    }
}
