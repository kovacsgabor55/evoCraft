using System;
using System.Collections.Generic;

namespace EvoCraft.Common
{
    public class AStarSearch
    {
        public Point StartLocation { get; set; }
        public Point EndLocation { get; set; }
        public List<Node> nodes { get; set; }
        public int Height;
        public int Width;

        /// <summary>
        /// Make a Search object for A Star search. It is independent of the original map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        public AStarSearch(Map map, Point startLocation, Point endLocation)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
            Height = map.Height;
            Width = map.Width;
            nodes = new List<Node>();

            foreach (Cell cell in map.Cells)
            {
                nodes.Add(
                    new Node(
                            cell.canBlockTypeBePlaced(BlockType.BlockOtherBlock)
                            ));
            }
        }

        /// <summary>
        /// Make a Search object for A Star search. It is independent of the original map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        public AStarSearch(Map map, Point startLocation, Point endLocation, BlockType blockT)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
            Height = map.Height;
            Width = map.Width;
            nodes = new List<Node>();

            foreach (Cell cell in map.Cells)
            {
                nodes.Add(
                    new Node(
                            cell.canBlockTypeBePlaced(blockT)
                            ));
            }
        }

        /// <summary>
        /// The main Search method.
        /// </summary>
        /// <returns>The direction to go to</returns>
        public Direction FindPathAndGiveDirection()
        {
            Direction direction = Direction.None;
            List<Node> open = new List<Node>();
            GetNodeAt(StartLocation).State = NodeState.Open;
            GetNodeAt(EndLocation).IsWalkable = true;
            open.Add(GetNodeAt(StartLocation));

            while (true)
            {
                Node current = Node.GetNodeWithTheLowestFCost(open);
                if (current != null)
                {
                    open.Remove(current);
                    current.State = NodeState.Closed;

                    if (GetPosition(current).Equals(EndLocation))
                    {
                        direction = GetDirectionFromTracingParentsOfLastNode();
                        break;
                    }

                    List<Node> goodAdjNodes = GetAdjacentWalkableNonClosedNodes(current);

                    open.AddRange(goodAdjNodes);
                }
                else
                {
                    break;
                }
                

            }


            // THIS CODE WAS USED FOR DEBUGGING
            //PrintMapWithPath();
            //switch (direction)
            //{
            //    case Direction.Up: Console.WriteLine("Go UP!"); break;
            //    case Direction.Down: Console.WriteLine("Go DOWN!"); break;
            //    case Direction.Left: Console.WriteLine("Go LEFT!"); break;
            //    case Direction.Right: Console.WriteLine("Go RIGHT!"); break;
            //    case Direction.None: Console.WriteLine("Stay!"); break;
            //}

            return direction;
        }

        /// <summary>
        /// Does what the name sais...
        /// </summary>
        /// <returns></returns>
        private Direction GetDirectionFromTracingParentsOfLastNode()
        {
            Node tracingNode = GetNodeAt(EndLocation);
            Node prevNode = null;
            while (true)
            {
                if (tracingNode.ParentNode != null)
                {
                    prevNode = tracingNode;
                    tracingNode = tracingNode.ParentNode;
                }
                else
                {
                    if (prevNode == null)
                    {
                        return Direction.None;
                    }
                    else
                    {
                        Point from = StartLocation;
                        Point to = GetPosition(prevNode);
                        if (to.x > from.x)
                        {
                            return Direction.Down;
                        }
                        if (to.x < from.x)
                        {
                            return Direction.Up;
                        }
                        if (to.y > from.y)
                        {
                            return Direction.Right;
                        }
                        if (to.y < from.y)
                        {
                            return Direction.Left;
                        }
                    }
                }
                
            }
            
        }
        

        /// <summary>
        /// Gets a node on the given position x and y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Node GetNodeAt(int x, int y)
        {
            return nodes[x + y * Height];
        }

        /// <summary>
        /// Gets a node on the given Point.
        /// </summary>
        /// <returns></returns>
        public Node GetNodeAt(Point p)
        {
            return nodes[p.x + p.y * Height];
        }

        /// <summary>
        /// Gets the position of the cell in the 2d space
        /// </summary>
        /// <param name="node">The node to test</param>
        /// <returns>The point at which the cell is</returns>
        public Point GetPosition(Node node)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (GetNodeAt(i,j) == node)
                    {
                        Point pt = new Point(i, j);
                        return pt;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the non-closed, walkable adjacent nodes.
        /// </summary>
        /// <param name="fromNode"></param>
        /// <returns></returns>
        private List<Node> GetAdjacentWalkableNonClosedNodes(Node fromNode)
        {
            List<Node> adjacentWalkableNodes = new List<Node>();
            IEnumerable<Point> nextLocations = GetAdjacentLocations(GetPosition(fromNode));

            foreach (var location in nextLocations)
            {
                int x = location.x;
                int y = location.y;


                Node node = GetNodeAt(x, y);
                // Ignore non-walkable nodes
                if (!node.IsWalkable)
                    continue;

                // Ignore already-closed nodes
                if (node.State == NodeState.Closed)
                    continue;

                // Already-open nodes are only added to the list if their G-value is lower going via this route.
                if (node.State == NodeState.Open)
                {
                    int gTemp = fromNode.G + 1;
                    if (gTemp < node.G)
                    {
                        node.ParentNode = fromNode;
                        node.G = gTemp;
                    }
                }
                else
                {
                    // If it's untested, set the parent and flag it as 'Open' for consideration
                    node.ParentNode = fromNode;
                    node.G = node.ParentNode.G + 1;
                    node.H = GetHeuristicDistanceFromEndPoint(location);
                    node.State = NodeState.Open;
                    adjacentWalkableNodes.Add(node);
                }
            }

            return adjacentWalkableNodes;
        }        

        /// <summary>
        /// Gets the adjacent positions to the given location. Returns only those points which fall inside the map boundarries.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private List<Point> GetAdjacentLocations(Point location)
        {
            List<Point> adjacents = new List<Point>();

            if (location.x + 1 < Height)
                adjacents.Add(new Point(location.x + 1, location.y));
            if (location.x - 1 >= 0)
                adjacents.Add(new Point(location.x - 1, location.y));
            if (location.y + 1 < Width)
                adjacents.Add(new Point(location.x, location.y + 1));
            if (location.y - 1 >= 0)
                adjacents.Add(new Point(location.x, location.y - 1));

            return adjacents;
        }
        

        /// <summary>
        /// Gets the heuristic distance from the endpoint.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private int GetHeuristicDistanceFromEndPoint(Point position)
        {
            return Math.Abs(position.x - EndLocation.x) + Math.Abs(position.y - EndLocation.y);
        }


        /// <summary>
        /// Prints the map with the path on it. Was used for debugging.
        /// </summary>
        private void PrintMapWithPath()
        {
            List<Point> path = new List<Point>();

            Node tracingNode = GetNodeAt(EndLocation);
            Node prevNode = null;
            while (true)
            {
                if (tracingNode.ParentNode != null)
                {
                    if (prevNode != null)
                    {
                        path.Add(GetPosition(tracingNode));
                    }
                    prevNode = tracingNode;
                    tracingNode = tracingNode.ParentNode;
                }
                else
                {
                    break;
                }

            }

            for (int i=0; i< Height; i++)
            {
                for (int j=0; j<Width; j++)
                {
                    if (i == StartLocation.x && j == StartLocation.y)
                    {
                        Console.Write("S ");
                    }
                    else if (i == EndLocation.x && j == EndLocation.y)
                    {
                        Console.Write("E ");
                    }
                    else if (new Point(i,j).IsInList(path))
                    {
                        Console.Write("O ");
                    }
                    else if (GetNodeAt(i, j).IsWalkable)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write("X ");
                    }
                    
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }
    }
}
