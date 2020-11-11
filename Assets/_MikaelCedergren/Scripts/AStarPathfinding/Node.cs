namespace MCUtility.Pathfinding {

    public class Node<T> : IHeapItem<Node<T>> {

        public int gCost;
        public int hCost;
        public int fCost { get { return gCost + hCost; } }

        public int HeapIndex { get; set; }

        public T data;

        public Node<T> parent;

        public Edge<T>[] edges; //Nodes leading OUT from this node.

        public Node() { }

        public int CompareTo(Node<T> other) {
            int compare = fCost.CompareTo(other.fCost);
            if (compare == 0) {
                compare = hCost.CompareTo(other.hCost);
            }

            return -compare;
        }

    }

}