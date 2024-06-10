using Containervervoer;
{
    var containers = new List<Containers>
{
    new Containers(10, ContainerType.Standard),
    new Containers(20, ContainerType.Valuable),
    new Containers(15, ContainerType.Coolable),
    new Containers(5, ContainerType.Standard)
};

    Console.WriteLine("Enter the number of rows:");
    int rows = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter the number of columns:");
    int columns = int.Parse(Console.ReadLine());

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < columns; j++)
        {
            Console.Write("i ");
        }
        Console.WriteLine();
    }
}