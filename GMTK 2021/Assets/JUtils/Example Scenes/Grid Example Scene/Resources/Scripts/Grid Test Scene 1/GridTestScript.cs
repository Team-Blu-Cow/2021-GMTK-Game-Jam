using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JUtil;
using JUtil.Grids;
using UnityEngine.Tilemaps;

// TEST GRID CONTROLLER MONOBEHAVIOUR ***************************************************************************************************************
public class GridTestScript : MonoBehaviour
{
    // MEMBERS ************************************************************************************
    [SerializeField] Grid<GridNode>[] grids;

    [SerializeField] NodeOverrides nodeOverrides;

    [SerializeField] private TileDatabase tileData;

    private Pathfinder<GridNode> pathfinder;

    [SerializeField] private DebugSettings debugSettings;

    // START METHOD *******************************************************************************
    void Start()
    {
        tileData.Init();

        if (grids.Length <= 0)
        { 
            Debug.LogError("no grids available");
            return;
        }

        foreach(var grid in grids)
        {
            grid.Init();
            InitGrid(grid);
        }

        // TODO: pathfinder only works with a single grid, it should work with multiple
        pathfinder = new Pathfinder<GridNode>(grids[0]);

        foreach(var link in nodeOverrides.gridLinks)
        {
            GridNode node1 = grids[link.grid1.index][link.grid1.position];
            GridNode node2 = grids[link.grid2.index][link.grid2.position];

            OverrideNode(node1, node2, link.grid1);
            OverrideNode(node2, node1, link.grid2);
        }
        
    }

    private void OverrideNode(GridNode node, GridNode partner, LinkID link)
    {
        node.overridden = true;
        node.overriddenDir = link.direction;
        node.Neighbors[link.direction].connected = true;
        node.Neighbors[link.direction].oneway = false;
        node.Neighbors[link.direction].overridden = true;
        node.Neighbors[link.direction].reference = partner;
    }

    // GRID INITIALISATION METHODS ****************************************************************
    private void InitGrid(Grid<GridNode> grid)
    {
        // TODO: doing this in two loops is kinda yuck, there is probably a better way of doing this.
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                CreateGridNode(x, y, grid);
            }
        }


        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                PreCalculateNeighbours(x, y, grid);
            }
        }
    }

    // after building and populating the grid with nodes, calculate all neighbouring node links
    // and attach them to each node
    private void PreCalculateNeighbours(int x, int y, Grid<GridNode> grid)
    {
        foreach (NodeNeighbor<GridNode> neighbor in grid[x, y].Neighbors)
        {
            // if node is not supposed to have neighbour in this direction
            if (neighbor.offsetVector == Vector2.zero)
                continue;

            GridNode neighbourNode = grid.GetNodeRelative(
                x,
                y,
                Mathf.RoundToInt(neighbor.offsetVector.x),
                Mathf.RoundToInt(neighbor.offsetVector.y)
                );

            if (neighbourNode == null)
            {
                neighbor.connected = false;
                continue;
            }

            if (neighbourNode.walkable != grid[x, y].walkable)
            {
                neighbor.connected = false;
                continue;
            }

            bool matching = false;
            bool neighborIsOneway = false;

            // TODO: this next stuff is kinda gross.. there is probably a better way of doing this.

            // Search through all neighbour directions to find this nodes opposite (so both
            // neighbour vectors point at each other) and check that both these connections are active.
            // figure out whether or not the node should be connected based on whether or not
            // both connections are/aren't oneway connections.
            foreach (NodeNeighbor<GridNode> newNeighbor in neighbourNode.Neighbors)
            {
                if (neighbor.offsetVector == newNeighbor.offsetVector * -1)
                {
                    if (neighbor.connected && newNeighbor.connected)
                    {
                        matching = true;
                        neighbor.connected = true;
                        if (newNeighbor.oneway)
                        {
                            matching = false;
                            neighborIsOneway = true;
                        }

                        if (neighbor.oneway)
                            newNeighbor.connected = false;

                        break;
                    }

                    break;
                }
            }

            if (matching == true)
                neighbor.reference = neighbourNode;

            if (neighbor.reference == null && !neighborIsOneway)
                neighbor.connected = false;
        }
    }

    private void SetNeighborVectors(GridNode node, TileDataObject tileDataObject = null)
    {
        float angle = 0;
        float addition = 360 / 8;

        if (tileDataObject != null)
        {
            for (int i = 0; i < node.Neighbors.neighbors.Length; i++)
            {
                node.Neighbors[i].connected = (tileDataObject.neighbours[i] != STATE.OFF);
                node.Neighbors[i].oneway = (tileDataObject.neighbours[i] == STATE.ONEWAY);

                Vector2 direction = Vector2.up.Rotate(angle);
                if (node.Neighbors[i].connected)
                    node.Neighbors[i].offsetVector = direction;

                angle += addition;
            }
        }
    }

    private void CreateGridNode(int x, int y, Grid<GridNode> grid)
    {
        bool walkable                   = false;
        int tilecount                   = 0;
        TileDataObject tileDataObject   = null;


        foreach (Tilemap tilemap in tileData.tilemaps)
        {
            Vector3Int currentTile = tilemap.WorldToCell(grid.ToWorld(x, y));

            if (tilemap.HasTile(currentTile))
            {
                if (tileData.TileHasData(tilemap, currentTile))
                {
                    tileDataObject = tileData[tilemap.GetTile(currentTile)];
                    tileDataObject.data.GetDataBool("walkable", out walkable);
                }
                tilecount++;
            }
        }

        grid[x, y]              = new GridNode(grid, x, y);
        grid[x, y].walkable     = walkable;
        grid[x, y].Neighbors    = new NodeNeighborhood<GridNode>(8);

        if (tilecount > 0)
            SetNeighborVectors(grid[x, y], tileDataObject);
    }

    // DEBUG DRAWING METHODS **********************************************************************
    private void OnDrawGizmos()
    {
        foreach(var grid in grids)
        {
            if (debugSettings.drawGrid)
                grid.DrawGizmos(debugSettings.drawGridColour, debugSettings.drawGridOutlineColour);

            if (debugSettings.drawNodes)
                DrawNodes(grid);
        }

        foreach (var link in nodeOverrides.gridLinks)
        {
            Gizmos.color = Color.yellow;
            if (!UnityEngine.Application.isPlaying && debugSettings.drawNodes)
            {
                Gizmos.DrawSphere(
                    grids[link.grid1.index].ToWorld(link.grid1.position),
                    grids[link.grid1.index].CellSize / 8
                    );
                Gizmos.DrawSphere(
                    grids[link.grid2.index].ToWorld(link.grid2.position),
                    grids[link.grid2.index].CellSize / 8
                    );
            }

            if (debugSettings.drawNodeConnections && debugSettings.drawNodes)
                Gizmos.DrawLine(
                    grids[link.grid1.index].ToWorld(link.grid1.position),
                    grids[link.grid2.index].ToWorld(link.grid2.position)
                    );
        }

    }

    private void DrawNodes(Grid<GridNode> grid)
    {
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                DrawNode(x, y, grid);
            }
        }
    }

    private void DrawNode(int x, int y, Grid<GridNode> grid)
    {
        Gizmos.color = new Color(1, 1, 1, 0.25f);

        if (grid.NodeExists(x, y))
        {
            Gizmos.color = (grid[x, y].overridden) ? Color.yellow : (grid[x, y].walkable) ? Color.blue : Color.red;
        }

        Gizmos.DrawSphere(grid.ToWorld(x, y), 1 * (grid.CellSize / 8));



        if (debugSettings.drawNodeConnections && grid.NodeExists(x, y))
        {
            foreach (NodeNeighbor<GridNode> neighbor in grid[x, y].Neighbors)
            {

                if (!neighbor.connected && !neighbor.overridden)
                    continue;

                if (neighbor.reference == null && !neighbor.overridden)
                    continue;


                Gizmos.color = (neighbor.oneway) ? Color.red : Color.blue;

                if (neighbor.overridden)
                    Gizmos.color = Color.yellow;

                Gizmos.DrawLine(
                    grid.ToWorld(x, y),
                    grid.ToWorld(x, y) + (new Vector3(neighbor.offsetVector.x, neighbor.offsetVector.y, grid.ToWorld(x, y).z) * (grid.CellSize / 2))
                    );

            }
        }
    }

    // PATHFINDING METHODS ************************************************************************
    public Vector3[] GetPath(int x, int y, int end_x, int end_y) => GetPath(grids[0].ToWorld(x, y), grids[0].ToWorld(end_x, end_y));
    public Vector3[] GetPath(Vector2Int start, Vector2Int end) => GetPath(grids[0].ToWorld(start.x, start.y), grids[0].ToWorld(end.x, end.y));
    public Vector3[] GetPath(Vector3 start, Vector3 end)
    {
        if(!grids[0].NodeExistsAt(start) || !grids[0].NodeExistsAt(end))
        {
            Debug.LogWarning("trying to pathfind to non existent nodes");
            return null;
        }

        return pathfinder.FindPath(start, end);
    }


    // MISC METHODS *******************************************************************************
    public Grid<GridNode> Grid(int i)
    {
        return grids[i];
    }

}

// TILE DATABASE ************************************************************************************************************************************
[System.Serializable]
public class TileDatabase
{
    [SerializeField] private List<TileDataObject> tileData;
    [SerializeField] public List<Tilemap> tilemaps;

    private Dictionary<TileBase, TileDataObject> dataFromTiles;

    public TileDataObject this[TileBase t]
    {
        get { return dataFromTiles[t]; }
    }

    public void Init()
    {
        dataFromTiles = new Dictionary<TileBase, TileDataObject>();

        foreach (var _tileData in tileData)
        {
            foreach (var tile in _tileData.tiles)
            {
                dataFromTiles.Add(tile, _tileData);
            }
        }
    }

    public bool TileHasData(Tilemap map, Vector3Int currentCell)
    {
        TileBase currentTile = map.GetTile(currentCell);
        if (currentTile != null && dataFromTiles.ContainsKey(currentTile))
            return true;
        return false;
    }
}

// GRID DEBUG SETTINGS CLASS ********************************************************************************************************************
[System.Serializable]
public class DebugSettings
{
    [Header("Gizmo Settings")]
    public bool drawGrid;
    public Color drawGridOutlineColour;
    public Color drawGridColour;
    [Space(5)]
    public bool drawNodes;
    public bool drawNodeConnections;

    public DebugSettings()
    {
        drawGrid = false;
        drawGridOutlineColour = Color.white;
        drawGridColour = new Color(1, 1, 1, 0.1f);
    }
}

// GRID NODE OVERRIDER CLASS ************************************************************************************************************************
[System.Serializable]
public class NodeOverrides
{
    [SerializeField] public GridLink[] gridLinks;
}

// INTER-GRID LINKS *********************************************************************************************************************************
[System.Serializable]
public struct GridLink
{
    public LinkID grid1;
    public LinkID grid2;
}

[System.Serializable]
public struct LinkID
{
    public int index;
    public Vector2Int position;
    [Range(0,7)] public int direction;
}

