namespace Containervervoer
{
    public enum ContainerType
    {
        Standard,
        Valuable,
        Coolable
    }

    public class Containers
    {
        public double Weight { get; private set; }
        public ContainerType Type { get; set; }

        public Containers(double weight, ContainerType type)
        {
            Weight = weight;
            Type = type;            
        }
    }
}
