using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlowField
{

    public Vector2Int GridStartPoint;
    public Cell[,] Grid { get; private set; }
    public Vector2Int GridSize { get; private set; }
    public float CellRadius { get; private set; }
    public int MaxBestCost { get; private set; }

    public Cell DestinationCell;
    private Queue<Cell> cellsToCheck = new Queue<Cell>();
    private float cellDiameter;

    public FlowField(float cellRadius, Vector2Int gridSize, Vector2Int gridStartPoint)
    {
        CellRadius = cellRadius;
        cellDiameter = cellRadius * 2f;
        GridSize = gridSize;
        MaxBestCost = Math.Max(gridSize.x, gridSize.y);
        GridStartPoint = gridStartPoint;
    }

    public void CreateFlowField()
    {
        CalculateFlowField();
        // flowField를 만들어주고 목적 cell의 cost를 1로
        DestinationCell.Cost = 1;
    }

    private void CalculateFlowField()
    {
        foreach (Cell curCell in Grid)
        {
            CalculateDirection(curCell, curCell.BestCost, curCell.AllNeighbor);
        }
    }

    private void CalculateDirection(Cell curCell, int bestCost, List<Cell> neighbors)
    {
        foreach (Cell curNeighbor in neighbors)
        {
            if (curNeighbor.BestCost < bestCost)
            {
                bestCost = curNeighbor.BestCost;
                curCell.BestDirection = GridDirection.GetDirectionFromV2I(curNeighbor.GridIndex - curCell.GridIndex);
            }
        }
    }

    public void CreateGrid()
    {
        Grid = new Cell[GridSize.x, GridSize.y];
        Vector3 cellHalfExtents = Vector3.one * CellRadius;
        // 벽이나 장애물 있을 때 사용
        int obstacleMask = LayerMask.GetMask("Obstacle");
        for (int x = 0; x < GridSize.x; ++x)
        {
            for (int y = 0; y < GridSize.y; ++y)
            {
                Vector3 worldPos = new Vector3(GridStartPoint.x + cellDiameter * x + CellRadius, 0, GridStartPoint.y + cellDiameter * y + CellRadius);
                var curCell = new Cell(worldPos, new Vector2Int(x, y));
                Grid[x, y] = curCell;
            }
        }

        // 이웃 셀 캐싱
        foreach (Cell curCell in Grid)
        {
            // 장애물 미리 설정
            Collider[] obstacles = Physics.OverlapBox(curCell.WorldPos, cellHalfExtents, Quaternion.identity, obstacleMask);
            if (obstacles.Length > 0)
            {
                curCell.IncreaseCost(255);
                curCell.BestCost = ushort.MaxValue;
                Cell obstacle = GetCellFromWorldPos(obstacles[0].transform.position);
                Vector2Int dir = curCell.GridIndex - obstacle.GridIndex;
                dir.x = (int)Mathf.Sign(dir.x);
                dir.y = (int)Mathf.Sign(dir.y);
                GridDirection get = GridDirection.GetDirectionFromV2I(dir);
                curCell.BestDirection = get;
                curCell.IsObstacle = true;
            }
        }

        foreach (Cell curCell in Grid)
        {
            curCell.AllNeighbor = GetNeighborCells(curCell.GridIndex, GridDirection.AllDirections);
            curCell.CardinalNeighbors = curCell.AllNeighbor.Where(x => (x.GridIndex - curCell.GridIndex).sqrMagnitude == 1).ToList();
        }
    }

    public void CreateCostField()
    {
        foreach (Cell curCell in Grid)
        {
            curCell.Cost = curCell.IsObstacle ? byte.MaxValue : (byte)1;
            curCell.BestCost = ushort.MaxValue;
        }
    }

    public void CreateIntegrationField(Cell destinationCell)
    {
        DestinationCell = destinationCell;
        destinationCell.Cost = 0;
        DestinationCell.BestCost = 0;
        DestinationCell.BestDirection = GridDirection.None;

        cellsToCheck.Clear();
        cellsToCheck.Enqueue(DestinationCell);

        while (cellsToCheck.Count > 0)
        {
            Cell curCell = cellsToCheck.Dequeue();
            if(curCell.IsObstacle) continue;
            CalculateBestScore(curCell);
        }
    }

    private void CalculateBestScore(Cell curCell)
    {
        List<Cell> neighbors = curCell.CardinalNeighbors;

        foreach (Cell curNeighbor in neighbors)
        {
            if (curCell.IsObstacle) continue;
            if (curNeighbor == curCell) continue;

            if (curNeighbor.Cost + curCell.BestCost < curNeighbor.BestCost)
            {
                curNeighbor.BestCost = (ushort)(curNeighbor.Cost + curCell.BestCost);
                cellsToCheck.Enqueue(curNeighbor);
            }
        }
    }

    private List<Cell> GetNeighborCells(Vector2Int nodeIndex, List<GridDirection> directions)
    {
        List<Cell> neighbors = new List<Cell>();
        foreach (var curDirection in directions)
        {
            Cell newNeighbor = GetCellAtRelativePos(nodeIndex, curDirection);


            // 장애물은 포함시키지 않는다.
            if (newNeighbor != null && newNeighbor.IsObstacle == false)
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
