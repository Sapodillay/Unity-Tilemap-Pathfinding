using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid2D : MonoBehaviour
{
    public Vector3Int gridWorldSize;
    public Vector3Int gridPosition;

    Tilemap _tileMap;
    Node[,] grid;



    private void Start()
    {
        _tileMap= GetComponent<Tilemap>();
        gridWorldSize = _tileMap.cellBounds.size;
        gridPosition = _tileMap.cellBounds.position + Vector3Int.RoundToInt(gameObject.transform.position);
        CreateGrid();
    }

    /// <summary>
    /// Populates grid array with nodes and sets walkable bool depending on if it has an active tile.
    /// </summary>
    void CreateGrid()
    {
        //initalize grid array
        grid = new Node[gridWorldSize.x, gridWorldSize.y];
        for (int x = 0; x < gridWorldSize.x; x++)
        {
            for (int y = 0; y < gridWorldSize.y; y++)
            {
                bool walkable = !_tileMap.HasTile(new Vector3Int(x, y, 0) + gridPosition);
                grid[x, y] = new Node(walkable, new Vector2(x, y), gridPosition);
            }
        }

    }

    /// <summary>
    /// Get neighbours around a node including diagonals 
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {

                //Skip current code
                if (x == 0 && y == 0)
                    continue;

                int checkX = (int)node.gridPosition.x + x;
                int checkY = (int)node.gridPosition.y + y;

                //Check if node is within the grid borders
                if (checkX >= 0 && checkX < gridWorldSize.x && checkY >= 0 && checkY < gridWorldSize.y)
                {
                    //Add node to nieghbours array
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    /// <summary>
    /// Gets a node from a world position
    /// </summary>
    /// <param name="vec">The world vector</param>
    /// <returns>A node</returns>
    public Node getNodeFromWorldPos(Vector3 vec)
    {
        Vector3Int tileVec = _tileMap.WorldToCell(vec);
        //Convert tile from world to local
        tileVec = tileVec - gridPosition;
        //Clamp positioning to stop out of bounds errors (Add error handling later)
        tileVec = new Vector3Int(Mathf.Clamp(tileVec.x, 0, gridWorldSize.x - 1), Mathf.Clamp(tileVec.y, 0, gridWorldSize.y - 1), 0);

        //Convert the tile vector to a node
        Node node = grid[tileVec.x, tileVec.y];
        return node;
    }
}
