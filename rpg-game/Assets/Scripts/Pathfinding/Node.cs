using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int posX;
    public int posY;

    public int gScore = 0;
    public int hScore = 0;

    public int FScore {get => gScore + hScore; }

    public Vector2 posInWorld;

    public bool isOnSearch;

    public bool isNonWalkable;

    public Node previousNode;

    public Node PreviousNode{ get => previousNode; set => previousNode = value; }

    public Node(int pX, int pY, bool noWalk, Vector2 pIW, Node pNode = null)
    {
        posX = pX;
        posY = pY;
        isNonWalkable = noWalk;
        posInWorld = pIW;

        if(pNode != null)
        {
            previousNode = pNode;
        }
    }

}
