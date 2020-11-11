using System.Collections.Generic;
using UnityEngine;

namespace MCUtility.Pathfinding {

    public class TileGraph {

        // This class constructs a simple path-finding compatible graph of the world
        // Each tile is a node, each neighbour from a tile is linked via an edge connection
        private Dictionary<Tile, Node<Tile>> graph;
        public static Dictionary<Tile, Node<Tile>> Graph => Instance.graph;

        private static TileGraph instance;
        private static TileGraph Instance {
            get {
                if (instance == null) {
                    new TileGraph();
                }
                return instance;
            }
        }

        private TileGraph() {
            instance = this;
            GenerateTileGraph();
        }

        private void GenerateTileGraph() {

            graph = new Dictionary<Tile, Node<Tile>>();

            // Loop through all tiles of the world
            // For each tile, create a node

            //  Do we create nodes for non-floor tiles? YES!
            //  Do we create nodes for tiles that are completely unwalkable (Walls..) YES!

            for (int x = 0; x < GameInstance.Width; x++) {
                for (int y = 0; y < GameInstance.Height; y++) {
                    Tile t = Tile.GetTileAt(x, y);
                    Node<Tile> n = new Node<Tile>();
                    n.data = t;
                    Graph.Add(t, n);
                }
            }

            foreach (Tile tile in Graph.Keys) {

                // Check current Path_Node
                Node<Tile> currentPath_Node = Graph[tile];

                List<Edge<Tile>> edges = new List<Edge<Tile>>();

                // get a list of neighbours for the tile
                Tile[] neighbours = tile.GetNeighbours();

                //foreach (var item in neighbours) {
                //    if (item != null)
                //        //Debug.Log(tile + " has neighbour " + item);

                //}

                // if neighbour is walkable, create an edge to the relevant node.
                for (int i = 0; i < neighbours.Length; i++) {
                    if (neighbours[i] != null) {
                        //This neighbour exist and is walkable so create an edge

                        //We have an edge, let's create it
                        Edge<Tile> edge = new Edge<Tile>();

                        //Adding the cost to ENTER the current edging tile.
                        edge.costToEnter = 1;

                        //Adding it to the list of edges.
                        edge.node = Graph[neighbours[i]];

                        // Add the edge to our temporary (and growable) list.
                        edges.Add(edge);
                    }
                }

                //Giving the current path_node the walkable neighbouring edges
                currentPath_Node.edges = edges.ToArray();

            }// Now loop through the rest of the remaining tile.
        }

    }
}