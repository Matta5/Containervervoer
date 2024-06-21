using Microsoft.VisualStudio.TestTools.UnitTesting;
using Containervervoer; // Ensure this namespace matches your project structure

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
        Assert.IsTrue(stack.CanAddContainer(new Container(30, ContainerType.Regular))); // Exactly 120 tons
    }

    [TestMethod]
    public void ExceedsMaximumWeightOnTopOfContainer()
    {
        var stack = new Stack();
        stack.AddContainer(new Container(30, ContainerType.Regular));
        stack.AddContainer(new Container(30, ContainerType.Regular));
        stack.AddContainer(new Container(30, ContainerType.Regular));
        stack.AddContainer(new Container(30, ContainerType.Regular)); // This should be the limit
        Assert.IsFalse(stack.CanAddContainer(new Container(1, ContainerType.Regular))); // Exceeds 120 tons
    }
}
