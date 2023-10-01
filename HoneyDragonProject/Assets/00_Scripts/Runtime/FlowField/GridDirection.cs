using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridDirection
{
    public readonly Vector2Int Vector;

    private GridDirection(int x, int y)
    {
        Vector = new Vector2Int(x, y);
    }

    public static implicit operator Vector2Int(GridDirection direction)
    {
        return direction.Vector;
    }

    public static GridDirection GetDirectionFromV2I(Vector2Int vector)
    {
        return CardinalAndIntercardinalDirections.DefaultIfEmpty(None).FirstOrDefault(direction => direction == vector);
    }

    public static readonly GridDirection None = new GridDirection(0, 0);
    public static readonly GridDirection Top = new GridDirection(0, 1);
    public static readonly GridDirection Bottom = new GridDirection(0, -1);
    public static readonly GridDirection Right = new GridDirection(1, 0);
    public static readonly GridDirection Left = new GridDirection(-1, 0);    
    
    public static readonly GridDirection TopLeft = new GridDirection(-1, 1);
    public static readonly GridDirection TopRight = new GridDirection(1, 1);
    public static readonly GridDirection BottomLeft = new GridDirection(-1, -1);
    public static readonly GridDirection BottomRight = new GridDirection(1, 1);

    public static readonly List<GridDirection> CardinalDirections = new List<GridDirection> {
        Top, Bottom, Right, Left
    };

    public static readonly List<GridDirection> CardinalAndIntercardinalDirections = new List<GridDirection> {
        Top, Bottom, Right, Left, TopLeft, TopRight, BottomLeft, BottomRight
    };

    public static readonly List<GridDirection> AllDirections = new List<GridDirection>
    {
        None, Top, Bottom, Right, Left, TopLeft, TopRight, BottomLeft, BottomRight
    };
}
