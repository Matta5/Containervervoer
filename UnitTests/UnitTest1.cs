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
            stack.AddContainer(new Container(30, ContainerType.Regular)); // 30 tons
            stack.AddContainer(new Container(30, ContainerType.Regular)); // 30 tons
            stack.AddContainer(new Container(30, ContainerType.Regular)); // 30 tons
            stack.AddContainer(new Container(30, ContainerType.Regular)); // 30 tons
            // Now the stack is at its maximum of 120 tons
            Assert.IsFalse(stack.CanAddContainer(new Container(10, ContainerType.Regular)));
        }

        [TestMethod]
        public void CanAddContainerIfWithinMaxWeight()
        {
            var stack = new Stack();
            stack.AddContainer(new Container(30, ContainerType.Regular)); // 30 tons
            stack.AddContainer(new Container(30, ContainerType.Regular)); // 30 tons
            stack.AddContainer(new Container(30, ContainerType.Regular)); // 30 tons
            // 90 tons so far, can still add up to 30 tons
            Assert.IsTrue(stack.CanAddContainer(new Container(30, ContainerType.Regular))); // Adding 30 tons should be within 120 tons
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
            stack.AddContainer(new Container(10, ContainerType.Regular)); // 10 tons, regular
            Assert.IsTrue(stack.CanAddContainer(new Container(5, ContainerType.Valuable))); // Can add valuable container on top
        }
    }
}