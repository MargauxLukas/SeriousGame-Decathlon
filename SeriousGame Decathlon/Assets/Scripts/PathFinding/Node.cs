using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : IHeapItem<Node>
{
    public bool traversable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int movementPenality;

    public int gCost;
    public int hCost;
    public Node parent;

    int heapIndex;

    public Node(bool _notTraversable, Vector3 _worldPosition, int _gridX, int _gridY, int _penalty)
    {
        traversable = _notTraversable;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
        movementPenality = _penalty;
    }

    public int fCost
    {
        get{return gCost + hCost;}
    }

    public int HeapIndex
    {
        get { return heapIndex; }
        set { heapIndex = value; }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
