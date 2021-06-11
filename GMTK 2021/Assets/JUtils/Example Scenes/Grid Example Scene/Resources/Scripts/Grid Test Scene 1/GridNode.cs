namespace JUtil.Grids
{
    [System.Serializable]
    public class GridNode : IPathFindingNode<GridNode>, IHeapItem<GridNode>
    {
        // INTERFACES *****************************************************************************
        public int gCost { get; set; } = 0;
        public int hCost { get; set; } = 0;
        public int fCost { get { return gCost + hCost; } }

        public int heapIndex { get; set; } = 0;

        public GridNodePosition position { get; set; }

        NodeNeighborhood<GridNode> neighbors;
        public NodeNeighborhood<GridNode> Neighbors { get { return neighbors; } set { neighbors = value; } }
        public GridNode parent { get; set; }

        public int CompareTo(GridNode other)
        {
            int compare = fCost.CompareTo(other.fCost);
            if (compare == 0)
                compare = hCost.CompareTo(other.hCost);

            return -compare;
        }

        public bool IsTraversable()
        {
            return walkable;
        }

        // MEMBERS ********************************************************************************
        public bool walkable;
        public bool overridden;
        public int overriddenDir;

        // METHODS ********************************************************************************
        public GridNode(Grid<GridNode> grid, int x, int y)
        {
            position = grid.GetNodePosition(x,y);
            walkable = false;
            overridden = false;
        }
    }
}
