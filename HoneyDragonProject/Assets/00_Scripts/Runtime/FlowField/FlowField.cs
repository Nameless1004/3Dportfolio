using System.Collections.Generic;
using UnityEngine;

public class FlowField
{

    public Vector2Int GridStartPoint;
    public Cell[,] Grid { get; private set; }
    public Vector2Int GridSize { get; private set; }
    public float CellRadius { get; private set; }

    public Cell DestinationCell;
    private float cellDiameter;

    public FlowField(float cellRadius, Vector2Int gridSize, Vector2Int gridStartPoint)
    {
        CellRadius = cellRadius;
        cellDiameter = cellRadius * 2f;
        GridSize = gridSize;
        GridStartPoint = gridStartPoint;
    }

    public void CreateFlowField()
    {
        foreach (Cell curCell in Grid)
        {
            List<Cell> curNeighbors = curCell.Neighbors;
            int bestCost = curCell.BestCost;
            foreach (Cell curNeighbor in curNeighbors)
            {
                if (curNeighbor.BestCost < bestCost)
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

        for (int x = 0; x < GridSize.x; ++x)
        {
            for (int y = 0; y < GridSize.y; ++y)
            {
                Vector3 worldPos = new Vector3(GridStartPoint.x + cellDiameter * x + CellRadius, 0, GridStartPoint.y + cellDiameter * y + CellRadius);
                Grid[x, y] = new Cell(worldPos, new Vector2Int(x, y));
            }
        }

        // 이웃 셀 캐싱
        foreach(Cell curCell in Grid)
        {
            curCell.Neighbors = GetNeighborCells(curCell.GridIndex, GridDirection.AllDirections);
        }
    }

    public void CreateCostField()
    {
        Vector3 cellHalfExtents = Vector3.one * CellRadius;
        // 벽이나 장애물 있을 때 사용
        // int terrainMask = LayerMask.GetMask("Impassible", "RoughTerrain");
        foreach (Cell curCell in Grid)
        {
            curCell.Cost = 1;
            curCell.BestCost = ushort.MaxValue;
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
        DestinationCell.BestCost = 0;
        DestinationCell.BestDirection = GridDirection.None;

        Queue<Cell> cellsToCheck = new Queue<Cell>();
        cellsToCheck.Enqueue(DestinationCell);

        while (cellsToCheck.Count > 0)
        {
            Cell curCell = cellsToCheck.Dequeue();

            List<Cell> curNeighbors = curCell.Neighbors;
            foreach (Cell curNeighbor in curNeighbors)
            {
                if (curNeighbor == curCell) continue;
                if (curNeighbor.Cost == byte.MaxValue) continue;
                if (curNeighbor.Cost + curCell.BestCost < curNeighbor.BestCost)
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
            if (newNeighbor != null)
            {
                neighbors.Add(newNeighbor);
            }
        }

        return neighbors;
    }

    private Cell GetCellAtRelativePos(Vector2Int originPos, Vector2Int relativePos)
    {
        Vector2Int finalPos = GridStartPoint + originPos + relativePos;

        // 범위 체크
        if (finalPos.x < GridStartPoint.x || finalPos.x >= GridStartPoint.x + GridSize.x || finalPos.y < GridStartPoint.y || finalPos.y >= GridStartPoint.y + GridSize.y)
        {
            return null;
        }
        else
        {
            return Grid[finalPos.x - GridStartPoint.x, finalPos.y - GridStartPoint.y];
        }
    }

    public Cell GetCellFromWorldPos(Vector3 worldPos)
    {
        worldPos.x -= GridStartPoint.x;
        worldPos.z -= GridStartPoint.y;

        float percentX = worldPos.x / (GridSize.x * cellDiameter);
        float percentY = worldPos.z / (GridSize.y * cellDiameter);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.Clamp(Mathf.FloorToInt((GridSize.x) * percentX), 0, GridSize.x - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt((GridSize.y) * percentY), 0, GridSize.y - 1);
        return Grid[x, y];
    }
}
