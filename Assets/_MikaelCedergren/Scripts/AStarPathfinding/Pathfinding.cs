using System.Collections.Generic;
using UnityEngine;

namespace MCUtility.Pathfinding {
    public class Pathfinding {

        private Dictionary<Tile, Node<Tile>> grid { get { return TileGraph.Graph; } }
        public static bool UseHeap = false;
        private int maxHeapSize;

        private Heap<Node<Tile>> openSetHeap;
        private HashSet<Node<Tile>> closedSet;

        private Node<Tile> currentNode;
        private Node<Tile> startNode;
        private Node<Tile> targetNode;

        private Queue<Tile> Path { get; set; }
        private bool pathSuccess = false;

        private BaseCharacter baseCharacter;

        private bool getEndTile;

        public Pathfinding(Tile startTile, Tile targetTile, BaseCharacter character, bool getEndTile = true) {
            maxHeapSize = 250;
            openSetHeap = new Heap<Node<Tile>>(maxHeapSize);
            closedSet = new HashSet<Node<Tile>>();
            this.baseCharacter = character;
            this.getEndTile = getEndTile;
            FindPath(startTile, targetTile);
        }

        public void FindPath(Tile startTile, Tile targetTile) {
            if (startTile == null || targetTile == null) {
                return;
            }

            currentNode = null;

            Path = new Queue<Tile>();
            pathSuccess = false;

            startNode = grid[startTile];
            targetNode = grid[targetTile];

            openSetHeap.Clear();
            closedSet.Clear();

            openSetHeap.Add(startNode);

            int heapCounter = 0;
            while (openSetHeap.Count > 0) {
                currentNode = openSetHeap.RemoveFirst();
                heapCounter++;
                if (heapCounter > maxHeapSize) {
                    pathSuccess = false;
                    break;
                }

                closedSet.Add(currentNode);

                if (currentNode == targetNode) {
                    pathSuccess = true;
                    break;
                }

                foreach (Edge<Tile> neighbour in currentNode.edges) {
                    if (neighbour.node.data == targetTile && getEndTile == false) {
                    }
                    else
                    if (neighbour.node.data.IsWalkable(baseCharacter) == false || closedSet.Contains(neighbour.node)) {
                        continue;
                    }

                    int movementCostToNeighbour = currentNode.gCost + neighbour.costToEnter;
                    if (movementCostToNeighbour < neighbour.node.gCost || openSetHeap.Contains(neighbour.node) == false) {
                        neighbour.node.gCost = movementCostToNeighbour;
                        neighbour.node.hCost = GetDistance(neighbour.node, targetNode);
                        neighbour.node.parent = currentNode;

                        if (openSetHeap.Contains(neighbour.node) == false) {
                            openSetHeap.Add(neighbour.node);
                        }
                        else {
                            openSetHeap.UpdateItem(neighbour.node);
                        }
                    }
                }
            }
            if (pathSuccess == true) {
                RetracePath(startNode, targetNode);
            }
            else if (currentNode != null) {
                RetracePath(startNode, currentNode);
            }

        }

        private int GetDistance(Node<Tile> a, Node<Tile> b) {
            return Mathf.RoundToInt(Vector3.Distance(a.data.Position, b.data.Position));
        }

        public Tile Next() {
            if (Path.Count > 0) {
                Tile tile = Path.Dequeue();
                return tile;
            }
            return null;
        }

        public bool HasNext() {
            return Path.Count > 0;
        }

        public Tile Last() {
            int counter = 0;
            foreach (Tile tile in Path) {
                if (++counter == Path.Count) {
                    return tile;
                }
            }
            return null;
        }

        public void PreviewPath() {
            //if (pathSuccess == true) {
            Tile lastTile = null;
            foreach (Tile tile in Path) {
                tile.PreviewPath(true);
                lastTile = tile;
            }
            //lastTile.PreviewPath(false);
            //}
        }

        public Queue<Tile> GetPath() {
            if (pathSuccess == true) {
                return new Queue<Tile>(Path);
            }
            return new Queue<Tile>();
        }

        private void RetracePath(Node<Tile> startNode, Node<Tile> endNode) {
            List<Tile> path = new List<Tile>();
            Node<Tile> currentNode = endNode;

            while (currentNode != startNode) {
                if (getEndTile == false && currentNode == endNode) {

                }
                else {
                    path.Add(currentNode.data);
                }
                currentNode = currentNode.parent;
            }
            path.Reverse();
            foreach (Tile tile in path) {
                Path.Enqueue(tile);
            }
            //if (getEndTile == true) {
            //    Debug.Log("Getting the end tile");
            //    Path.Enqueue(endNode.data);
            //}
        }

        public override string ToString() {
            string returnString = string.Empty;
            foreach (var item in Path) {
                returnString += item.ToString();
                returnString += " ";
            }
            return returnString;
        }

    }


}
