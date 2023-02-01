using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid2D))]
public class Pathfinding : MonoBehaviour
{
    [SerializeField] Grid2D grid;

    /// <summary>
    /// A* Pathfinding
    /// 
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <returns>List of nodes in the order of the shortest path between the two nodes</returns>
    public List<Node> FindPath(Vector3 startPos, Vector3 endPos)
    {
        Node startNode = grid.getNodeFromWorldPos(startPos);
        Node targetNode = grid.getNodeFromWorldPos(endPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);


        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost)
                {
                    if (openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }

                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }


            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);

                    }

                }
            }
        }
        return null;
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }




    /// <summary>
    /// Get the distance between 2 nodes
    /// </summary>
    /// <param name="nodeA"></param>
    /// <param name="nodeB"></param>
    /// <returns>The distance between nodeA and nodeB</returns>
    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs((int)nodeA.gridPosition.x - (int)nodeB.gridPosition.x);
        int distY = Mathf.Abs((int)nodeA.gridPosition.y - (int)nodeB.gridPosition.y);


        if (distX > distY)
        {
            return 14*distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);

    }

}
