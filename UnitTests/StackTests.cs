using Containervervoer;

[TestClass]
public class StackTests
{
    [TestMethod]
    public void CanPlaceValuableContainerOnTopOfOthers_HappyPath()
    {
        var stack = new Stack();
        stack.AddContainer(new Container(10, ContainerType.Regular));
        Assert.IsTrue(stack.CanAddContainer(new Container(10, ContainerType.Valuable)));
    }

    [TestMethod]
    public void CannotStackAboveValuableContainer()
    {
        var stack = new Stack();
        stack.AddContainer(new Container(10, ContainerType.Valuable));
        Assert.IsFalse(stack.CanAddContainer(new Container(10, ContainerType.Regular)));
    }

    [TestMethod]
    public void MaximumWeightOnTopOfContainer_HappyPath()
    {
        var stack = new Stack();
        stack.AddContainer(new Container(30, ContainerType.Regular));
        stack.AddContainer(new Container(30, ContainerType.Regular));
        stack.AddContainer(new Container(30, ContainerType.Regular));
        Assert.IsTrue(stack.CanAddContainer(new Container(30, ContainerType.Regular))); // Exact 120 ton
    }

    [TestMethod]
    public void ExceedsMaximumWeightOnTopOfContainer()
    {
        var stack = new Stack();
        stack.AddContainer(new Container(1, ContainerType.Regular));
        stack.AddContainer(new Container(30, ContainerType.Regular));
        stack.AddContainer(new Container(30, ContainerType.Regular));
        stack.AddContainer(new Container(30, ContainerType.Regular));
        stack.AddContainer(new Container(30, ContainerType.Regular));
        Assert.IsFalse(stack.CanAddContainer(new Container(1, ContainerType.Regular))); // meer als 120 ton
    }
}
