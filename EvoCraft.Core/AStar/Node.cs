using System.Collections.Generic;

namespace EvoCraft.Core
{
    /// <summary>
    /// A "Cell" or "Tile" in the map used only for the Pathfinding.
    /// </summary>
    public class Node
    {
        public bool IsWalkable { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public int F { get { return this.G + this.H; } }
        public NodeState State { get; set; }
        public Node ParentNode { get; set; }
    
        public Node(bool isWalkable)
        {
            IsWalkable = isWalkable;
            G = 0;
            H = 0;
            State = NodeState.Untested;
            ParentNode = null;
        }

        /// <summary>
        /// Gets the node with the lowest F cost in the Nodelist. Is mainly used for getting the best potential next node for search.
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        public static Node GetNodeWithTheLowestFCost(List<Node> nodeList)
        {
            Node bestNode = null;
            foreach (Node node in nodeList)
            {
                if (bestNode == null)
                {
                    bestNode = node;
                }
                else
                {
                    if (bestNode.F > node.F || (bestNode.F == node.F && bestNode.H > node.H))
                    {
                        bestNode = node;
                    }
                }
            }
            return bestNode;
        }
    }

    
}
