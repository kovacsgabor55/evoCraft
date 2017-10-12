using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvoCraft.Common;
using System.Collections.Generic;

namespace EVOCraft.Common_UTest
{
    [TestClass]
    public class AStarTests
    {
        AStarSearch search = new AStarSearch(Engine.Map, new Point(2,4), new Point(Engine.Map.Height - 1, Engine.Map.Width - 1));
        
        [TestMethod]
        public void GetPositionTest()
        {
            Assert.AreEqual(search.StartLocation, search.GetPosition(search.GetNodeAt(search.StartLocation)));
            Assert.AreEqual(search.EndLocation, search.GetPosition(search.GetNodeAt(search.EndLocation)));
        }

        [TestMethod]
        public void GetNodeWithTheLowestFCostTest()
        {
            List<Node> nodeList = new List<Node>();
            Node n1 = new Node(true);
            Node n2 = new Node(true);
            Node n3 = new Node(true);
            n1.G = 3;
            n1.H = 5;

            n2.G = 2;
            n2.H = 7;

            n3.G = 6;
            n3.H = 4;
            nodeList.Add(n1);
            nodeList.Add(n2);
            nodeList.Add(n3);

            Assert.AreEqual(n1.G, Node.GetNodeWithTheLowestFCost(nodeList).G);
        }
    }
}
