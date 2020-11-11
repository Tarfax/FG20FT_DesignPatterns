namespace MCUtility.Pathfinding {
    public class Edge<T> {
        public int costToEnter; // Cost to traverse this edge (cost to ENTER the tile)
        public Node<T> node;
    }
}