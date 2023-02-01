using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 gridOffset;

    public bool walkable;
    public Vector2 gridPosition;
    //Get the position in the middle of the tile in world space
    public Vector2 worldPosition => gridPosition + new Vector2(gridOffset.x, gridOffset.y) + new Vector2(0.5f, 0.5f);

    public int gCost;
    public int hCost;
    public Node parent;


    public Node(bool _walkable, Vector2 gridPosition, Vector3 offset)
    {
        walkable = _walkable;
        this.gridPosition = gridPosition;
        this.gridOffset= offset;
    }

    public int fCost => gCost + hCost;

}
