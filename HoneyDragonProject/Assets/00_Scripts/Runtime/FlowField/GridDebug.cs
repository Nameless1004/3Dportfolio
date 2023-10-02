using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GridDebug : MonoBehaviour
{
    public enum FlowFieldDisplayType
    {
        CostField,
        IntegrationField,
        FlowField
    }


   // [Tooltip(" 0 : Left\n1 : Right\n2 : Top\n3 : Bottom\n4 : TopLeft\n5 : TopRight\n6 : BottomLeft\n7 : BottomRight\n")]
   // public Texture[] directionTexture;

    public bool displayGrid;
    public bool displayFlowField;
    public FlowFieldDisplayType CurDisplayType;
    public FlowField CurFlowField;
    private Vector2Int gridSize;
    private Vector2Int gridStartPoint;
    private float cellRadius;
    private GridController gridController;
    public Cell SelectedCell;

    public void SetFlowField(FlowField flowField, Vector2Int _gridStartPoint) {
        CurFlowField = flowField;
        //displayGrid = true;
        gridSize = CurFlowField.GridSize;
        cellRadius = CurFlowField.CellRadius;
        gridStartPoint = _gridStartPoint;
    }

    private void Awake()
    {
        gridController = GetComponent<GridController>();
    }

    private void OnDrawGizmos()
    {
        if(displayGrid)
        {
            if(CurFlowField == null)
            {
                DrawGrid(gridController.GridSize, Color.yellow, gridController.CellRadius);
            }
            else
            {
                DrawGrid(gridSize, Color.green, cellRadius);
            }
        }

        if (CurFlowField == null) return;

        if (displayFlowField)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;

            switch (CurDisplayType)
            {
                case FlowFieldDisplayType.CostField:
                    foreach (Cell curCell in CurFlowField.Grid)
                    {
                        Handles.Label(curCell.WorldPos, curCell.Cost.ToString(), style);
                    }
                    break;
                case FlowFieldDisplayType.IntegrationField:
                    foreach (Cell curCell in CurFlowField.Grid)
                    {
                        Handles.Label(curCell.WorldPos, curCell.BestCost.ToString(), style);
                    }
                    break;
                case FlowFieldDisplayType.FlowField:
                    foreach (Cell curCell in CurFlowField.Grid)
                    {
                        string name = GetDirectionIconName(curCell.BestDirection);
                        if (name == null) continue;

                        Vector3 pos = curCell.WorldPos + Vector3.up;
                        Gizmos.DrawIcon(pos, name + ".png");
                    }
                    break;
            }
        }
    }

    private void DrawGrid(Vector2Int drawGridSize, Color drawColor, float drawCellRadius)
    {
        Gizmos.color = drawColor;
        for(int x = 0; x < drawGridSize.x; x++) 
        {
            for(int y = 0; y < drawGridSize.y; y++)
            {
                Vector3 center = new Vector3(gridStartPoint.x + drawCellRadius * 2 * x + drawCellRadius, 0, gridStartPoint.y + drawCellRadius * 2 * y + drawCellRadius);
                Vector3 size = Vector3.one * drawCellRadius * 2;
                if(SelectedCell != null)
                {
                    if(SelectedCell.GridIndex.x == x && SelectedCell.GridIndex.y == y)
                    {
                        Gizmos.color = Color.red;
                    }
                    else
                        Gizmos.color = drawColor;
                }
                Gizmos.DrawWireCube(center, size);
            }
        }
    }

    private string GetDirectionIconName(GridDirection direction)
    {
        if(direction == GridDirection.Left)
        {
            return "Left";
        }
        else if(direction == GridDirection.Right)
        {
            return "Right";
        }
        else if(direction == GridDirection.Top)
        {
            return "Top";
        }
        else if(direction == GridDirection.Bottom)
        {
            return "Bottom";
        }
        else if (direction == GridDirection.TopLeft)
        {
            return "TopLeft";
        }
        else if (direction == GridDirection.TopRight)
        {
            return "TopRight";
        }
        else if(direction == GridDirection.BottomLeft)
        {
            return "BottomLeft";
        }
        else if(direction == GridDirection.BottomRight)
        {
            return "BottomRight";
        }

        return null;
    }
}
