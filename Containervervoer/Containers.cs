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
        public double Weight { get; private set; }
        public ContainerType Type { get; private set; }

        public Container(double weight, ContainerType type = ContainerType.Regular)
        {
            Weight = weight;
            Type = type;
        }

    }
}
