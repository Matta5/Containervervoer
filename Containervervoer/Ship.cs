namespace Containervervoer
{
    public class Ship
    {
        public double MaxWeight { get; private set; }
        public double CurrentWeight { get; private set; }
        public int Length { get; private set; }
        public int Width { get; private set; }
        public Stack<Containers>[,] Containers;

        public Ship(double maxWeight, int length, int width)
        {
            MaxWeight = maxWeight;
            Length = length;
            Width = width;
            Containers = new Stack<Containers>[length, width];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Containers[i, j] = new Stack<Containers>();
                }
            }
            CurrentWeight = 0;
        }

        public void AddContainer(Containers container, int row, int column)
        {
            if (row < 0 || row >= Length || column < 0 || column >= Width)
            {
                throw new ArgumentException("Invalid position.");
            }

            if (CurrentWeight + container.Weight > MaxWeight)
            {
                throw new InvalidOperationException("Adding this container would exceed the ship's maximum weight.");
            }

            Containers[row, column].Push(container);
            CurrentWeight += container.Weight;
        }

    }
}
