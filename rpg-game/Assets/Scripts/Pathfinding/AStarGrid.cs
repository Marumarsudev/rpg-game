using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarGrid : MonoBehaviour
{
    public bool drawGizmos = false;

    private Vector2 startPos;

    public Vector2 GridWorldSize;

    public int sizeX, sizeY;

    public float nodeRadius = 0.25f;

    public float castRadius = 0.5f;

    [Tooltip("0 for no updates || float updates every n second")]
    public float updateGridRuntime = 0;

    private Node[,] nodeGrid;

    public Node[,] NodeGrid  { get => nodeGrid; }

    public LayerMask wallMask;

    private void Start()
    {
        startPos = transform.position;

        sizeX = Mathf.RoundToInt(GridWorldSize.x / (nodeRadius * 2));
        sizeY = Mathf.RoundToInt(GridWorldSize.y / (nodeRadius * 2));

        if(updateGridRuntime > 0)
        {
            InvokeRepeating("ReCreateGrid", 0.0f, updateGridRuntime);
        }
        else
        {
            CreateGrid(sizeX, sizeY, nodeRadius);
        }
    }

    private void ReCreateGrid()
    {
        CreateGrid(sizeX, sizeY, nodeRadius);
    }

    private void CreateGrid(int sX, int sY, float nR)
    {
        nodeGrid = new Node[sX,sY];
        Vector2 bottomLeft = (Vector2)transform.position - Vector2.right * GridWorldSize.x / 2 - Vector2.up * GridWorldSize.y / 2;
        for (int y = 0; y < sY; y++)
        {
            for (int x = 0; x < sX; x++)
            {
                //Vector2 nodePosInWorld = new Vector2(startPos.x + (float)x * nodeRadius * 2, startPos.y + (float)y * nodeRadius * 2);
                Vector2 nodePosInWorld = bottomLeft + Vector2.right * (x * (nodeRadius * 2) + nodeRadius) + Vector2.up * (y * (nodeRadius * 2) + nodeRadius);
                bool iW = false;
                if(Physics2D.CircleCast(nodePosInWorld, castRadius, Vector2.one * castRadius, 0, wallMask))
                    iW = true;
                nodeGrid[x, y] = new Node(x, y, iW, nodePosInWorld);
            }
        }
    }

    private void DebugPrintGrid(int sX, int sY)
    {
        for (int y = 0; y < sY; y++)
        {
            for (int x = 0; x < sX; x++)
            {
                Debug.Log(" x: " + nodeGrid[x, y].posX + " y: " + nodeGrid[x, y].posY + " iW: " + nodeGrid[x, y].isNonWalkable + " PIW: " + nodeGrid[x, y].posInWorld);
            }
        }
    }

    void OnDrawGizmos()
    {
        if(nodeGrid != null && drawGizmos)
        {
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                if(nodeGrid[x, y].isNonWalkable)
                {
                    Gizmos.color = Color.red;
                }
                else if(nodeGrid[x, y].isOnSearch)
                {
                    Gizmos.color = Color.cyan;
                }
                else
                {
                    Gizmos.color = Color.white;
                }

                Gizmos.DrawCube(nodeGrid[x, y].posInWorld, Vector3.one / 5);
            }
        }
        }
    }
}
