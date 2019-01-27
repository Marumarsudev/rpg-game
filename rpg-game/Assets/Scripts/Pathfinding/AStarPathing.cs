using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathing : MonoBehaviour
{
    public bool debugDraw = false;

    private Node[,] nodeGrid;
    private AStarGrid aStarGrid;

    private void Start()
    {
        nodeGrid = GetComponent<AStarGrid>().NodeGrid;
        aStarGrid = GetComponent<AStarGrid>();
    }

    private Node WorldPositionToNode(Vector2 wPos)
    {
        float ixPos = ((wPos.x + aStarGrid.GridWorldSize.x / 2) / aStarGrid.GridWorldSize.x);
        float iyPos = ((wPos.y + aStarGrid.GridWorldSize.y / 2) / aStarGrid.GridWorldSize.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((aStarGrid.sizeX - 1) * ixPos);
        int iy = Mathf.RoundToInt((aStarGrid.sizeY - 1) * iyPos);

        return nodeGrid[ix, iy];
    }

    private void DebugDrawRoute(List<Vector2> route)
    {
        if(debugDraw)
        {
            for(int i = 0; i < route.Count - 1; i++)
            {
                Debug.DrawLine(route[i], route[i+1], Color.green, 2.0f);
            }
        }
    }

    private int DistanceBetweenNodes(Node a, Node b)
    {
        int ix = Mathf.Abs(a.posX - b.posX);
        int iy = Mathf.Abs(a.posY - b.posY);

        return ix + iy;
    }

    private List<Vector2> ReconstructRoute(Node sNode, Node node)
    {
        List<Vector2> route = new List<Vector2>();
        route.Add(node.posInWorld);
        while(node.previousNode != null)
        {
            node = node.previousNode;
            route.Add(node.posInWorld);
        }

        route.Reverse();

        DebugDrawRoute(route);

        return route;
    }

    public List<Vector2> GetRoute(Vector2 sPos, Vector2 ePos)
    {

        List<Node> openNodes = new List<Node>();
        HashSet<Node> closedNodes = new HashSet<Node>();

        //nodeGrid = GetComponent<AStarGrid>().NodeGrid;

        List<Vector2> route = new List<Vector2>();

        Node sNode = WorldPositionToNode(sPos);
        Node eNode = WorldPositionToNode(ePos);

        bool pathFound = false;

        for (int y = 0; y < nodeGrid.GetLength(1); y++)
        {
            for (int x = 0; x < nodeGrid.GetLength(0); x++)
            {
                nodeGrid[x, y].previousNode = null;
                nodeGrid[x, y].gScore = DistanceBetweenNodes(nodeGrid[x, y], sNode);
            }
        }

        openNodes.Add(sNode);

        while(openNodes.Count > 0 && !pathFound)
        {
            
            Node node = openNodes[0];

            for(int i = 1; i < openNodes.Count; i++)
            {
                if(openNodes[i].FScore < node.FScore || openNodes[i].FScore == node.FScore && openNodes[i].hScore < node.hScore)
                {
                    node = openNodes[i];
                }
            }


            openNodes.Remove(node);
            closedNodes.Add(node);


            if(node.posX == eNode.posX && node.posY == eNode.posY)
            {
                pathFound = true;
                return ReconstructRoute(sNode, node);
            }

            List<Node> neighborNodes = new List<Node>();

            for (int y = -1; y <= 1; y++)
            {
                for(int x = -1; x <= 1; x++)
                {
                    if(x == 0 && y == 0)
                        continue;

                    int checkX = node.posX + x;
                    int checkY = node.posY + y;


                    if(checkX >= 0 && checkX < nodeGrid.GetLength(0) && checkY >= 0 && checkY < nodeGrid.GetLength(1))
                    {
                        neighborNodes.Add(nodeGrid[checkX, checkY]);
                    }
                }
            }
            
            foreach (Node neighbor in neighborNodes)
            {
                if(neighbor.isNonWalkable || closedNodes.Contains(neighbor))
                            continue;

                        int tentative_gScore = node.gScore + DistanceBetweenNodes(node, neighbor);

                        if (!openNodes.Contains(neighbor))
                        {
                            openNodes.Add(neighbor);
                        }
                        else if (tentative_gScore >= neighbor.gScore)
                            continue;

                        neighbor.previousNode = node;
                        neighbor.gScore = tentative_gScore;
                        neighbor.hScore = DistanceBetweenNodes(neighbor, eNode);
            }

        }

        Debug.Log("No route found :(");

        return route;
    }

}
