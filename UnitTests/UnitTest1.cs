using System;
using System.Collections.Generic;
using System.Linq;
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
            stack.AddContainer(new Container(100, ContainerType.Regular)); // 100 tons
            Assert.IsFalse(stack.CanAddContainer(new Container(21, ContainerType.Regular))); // Adding 21 tons should exceed 120 tons
        }

        [TestMethod]
        public void CanAddContainerIfWithinMaxWeight()
        {
            var stack = new Stack();
            stack.AddContainer(new Container(50, ContainerType.Regular)); // 50 tons
            Assert.IsTrue(stack.CanAddContainer(new Container(70, ContainerType.Regular))); // Adding 70 tons should be within 120 tons
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