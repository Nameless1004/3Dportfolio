using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Cell(Vector3 worldPos, Vector2Int gridIndex) 
    {
        WorldPos = worldPos;
        GridIndex = gridIndex;
        Cost = 1;
        BestCost = ushort.MaxValue;
        BestDirection = GridDirection.None;
    }

    public Vector3 WorldPos;
    public Vector2Int GridIndex;
    public byte Cost;
    public ushort BestCost;
    public GridDirection BestDirection;

    public void IncreaseCost(int amt)
    {
        if (Cost == byte.MaxValue) return;
        if(amt + Cost >= 255) { Cost = byte.MaxValue; }
        else { Cost += (byte)amt; }
    }
}
