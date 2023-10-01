using System.Collections.Generic;
using UnityEngine;

public class FlowField
{
    public Vector2Int StartPoint;
    public Cell[,] Grid { get; private set; }
    public Vector2Int GridSize { get; private set; }
    public float CellRadius { get; private set; }

    public Cell DestinationCell;
    private float cellDiameter;

    public FlowField(float cellRadius, Vector2Int gridSize)
    {
        CellRadius = cellRadius;
        cellDiameter = cellRadius * 2f;
        GridSize = gridSize;
    }
    
    public void ResetBestCost()
    {
        for(int i = 0; i < GridSize.x; ++i)
        {
            for(int j = 0; j < GridSize.y; ++j)
            {
                Grid[i, j].BestCost = ushort.MaxValue;
            }
        }
    }

    public void CreateFlowField()
    {
        foreach(Cell curCell in Grid)
        {
            List<Cell> curNeighbors = GetNeighborCells(curCell.GridIndex, GridDirection.AllDirections);
            int bestCost = curCell.BestCost;
            foreach(Cell curNeighbor in curNeighbors)
            {
                if(curNeighbor.BestCost < bestCost)
                {
                    bestCost = curNeighbor.BestCost;
                    curCell.BestDirection = GridDirection.GetDirectionFromV2I(curNeighbor.GridIndex - curCell.GridIndex);
                }
            }
        }
    }

    public void CreateGrid()
    {
        Grid = new Cell[GridSize.x, GridSize.y];

        for(int x = 0; x < GridSize.x; ++x)
        {
            for (int y = 0; y < GridSize.y; ++y)
            {
                Vector3 worldPos = new Vector3(cellDiameter * x + CellRadius, 0, cellDiameter * y + CellRadius);
                Grid[x, y] = new Cell(worldPos, new Vector2Int(x, y));
            }
        }
    }

    public void CreateCostField()
    {
        Vector3 cellHalfExtents = Vector3.one * CellRadius;
        // 벽이나 장애물 있을 때 사용
        // int terrainMask = LayerMask.GetMask("Impassible", "RoughTerrain");
        foreach(Cell curCell in Grid)
        {
            // Collider[] obstacles = Physics.OverlapBox(curCell.WorldPos, cellHalfExtents, Quaternion.identity, terrainMask);
            //bool hasIncreaseCost = false;
            //foreach(Collider col in obstacles)
            //{
            //    if(col.gameObject.layer == 8)
            //    {
            //        curCell.IncreaseCost(255);
            //        continue;
            //    }
            //    else if(!hasIncreaseCost && col.gameObject.layer == 9)
            //    {
            //        curCell.IncreaseCost(3);
            //        hasIncreaseCost = true;
            //    }
            //}
        }
    }

    public void CreateIntegrationField(Cell destinationCell)
    {
        DestinationCell = destinationCell;
        DestinationCell.Cost = 0;
        DestinationCell.BestCost= 0;

        Queue<Cell> cellsToCheck = new Queue<Cell>();
        cellsToCheck.Enqueue(DestinationCell);

        while(cellsToCheck.Count > 0)
        {
            Cell curCell = cellsToCheck.Dequeue();
            List<Cell> curNeighbors = GetNeighborCells(curCell.GridIndex, GridDirection.CardinalDirections);

            foreach(Cell curNeighbor in curNeighbors)
            {
                if (curNeighbor.Cost == byte.MaxValue) continue;
                if(curNeighbor.Cost + curCell.BestCost < curNeighbor.BestCost)
                {
                    curNeighbor.BestCost = (ushort)(curNeighbor.Cost + curCell.BestCost);
                    cellsToCheck.Enqueue(curNeighbor);
                }
            }
        }
    }

    private List<Cell> GetNeighborCells(Vector2Int nodeIndex, List<GridDirection> directions)
    {
        List<Cell> neighbors = new List<Cell>();
        foreach (var curDirection in directions) 
        {
            Cell newNeighbor = GetCellAtRelativePos(nodeIndex, curDirection);
            if(newNeighbor != null)
            {
                neighbors.Add(newNeighbor);
            }
        }

        return neighbors;
    }

    private Cell GetCellAtRelativePos(Vector2Int originPos, Vector2Int relativePos)
    {
        Vector2Int finalPos = originPos + relativePos;

        // 범위 체크
        if(finalPos.x < 0 || finalPos.x >= GridSize.x || finalPos.y < 0 || finalPos.y >= GridSize.y)
        {
            return null;
        }
        else
        {
            return Grid[finalPos.x, finalPos.y];
        }
    }

    public Cell GetCellFromWorldPos(Vector3 worldPos)
    {
        float percentX = worldPos.x / (GridSize.x * cellDiameter);
        float percentY = worldPos.z / (GridSize.y * cellDiameter);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.Clamp(Mathf.FloorToInt((GridSize.x) * percentX), 0, GridSize.x - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt((GridSize.y) * percentY), 0, GridSize.y - 1);
        return Grid[x, y];
    }
}
