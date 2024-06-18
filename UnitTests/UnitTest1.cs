using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Containervervoer.Tests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void CannotAddContainerIfExceedsMaxWeight()
        {
            var stack = new Stack();
            stack.AddContainer(new Container(10, ContainerType.Cooled));
            stack.AddContainer(new Container(30, ContainerType.Regular));
            stack.AddContainer(new Container(30, ContainerType.Regular));
            stack.AddContainer(new Container(30, ContainerType.Regular));
            stack.AddContainer(new Container(30, ContainerType.Regular));
            // 120 tons on top of 1 container so far, cannot add 10 tons
            Assert.IsFalse(stack.CanAddContainer(new Container(10, ContainerType.Regular)));
        }

        [TestMethod]
        public void CanAddContainerIfWithinMaxWeight()
        {
            var stack = new Stack();
            stack.AddContainer(new Container(30, ContainerType.Regular));
            stack.AddContainer(new Container(30, ContainerType.Regular));
            stack.AddContainer(new Container(30, ContainerType.Regular));
            // 60 tons on top of 1 containerso so far, can still add up to 30 tons
            Assert.IsTrue(stack.CanAddContainer(new Container(30, ContainerType.Regular)));
        }

        [TestMethod]
        public void CannotAddContainerOnTopOfValuableContainer()
        {
            var stack = new Stack();
            stack.AddContainer(new Container(10, ContainerType.Valuable));
            Assert.IsFalse(stack.CanAddContainer(new Container(5, ContainerType.Regular)));
        }

        [TestMethod]
        public void CanAddValuableContainerOnTopOfOtherContainers()
        {
            var stack = new Stack();
            stack.AddContainer(new Container(10, ContainerType.Regular));
            Assert.IsTrue(stack.CanAddContainer(new Container(5, ContainerType.Valuable)));
        }
    }
}