

using UnityEngine;

public class IfMousePositionInRange : ConditionalNode
{
    public float Range = 5f;
    public Vector2 pivot;


    protected override bool IsUpdatable()
    {
        Debug.Log($"Condition Check {Vector2.Distance(pivot, Camera.main.ScreenToWorldPoint(Input.mousePosition))}");
        return Vector2.Distance(pivot, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < Range;
    }
}
