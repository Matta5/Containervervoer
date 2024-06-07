using Containervervoer;
using static System.Net.Mime.MediaTypeNames;

static void Main(string[] args)
{
    // Create some containers
    var containers = new List<Containers>
        {
            new Containers(10),
            new Containers(20, true),
            new Containers(15, false, true),
            new Containers(5)
        };

    // Create a cargo ship
    var ship = new Ship(200, 4, 4);

    // Add containers to the ship
    ship.AddContainer(containers[0], new Position(0, 0));
    ship.AddContainer(containers[1], new Position(0, 1));
    ship.AddContainer(containers[2], new Position(1, 0));
    ship.AddContainer(containers[3], new Position(1, 1));

    // Visualize the layout
    var planner = new LayoutPlanner();
    planner.VisualizeLayout(ship);

    // Run tests
    //var tests = new UnitTest1();
    //tests.TestMaxWeightPerContainer();
    //tests.TestValuableContainersAccessible();
    //tests.TestRefrigeratedContainersInFirstRow();
    //tests.TestMinimumWeightUsed();
    //tests.TestBalanced();
}