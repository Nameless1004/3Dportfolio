using UnityEngine;

public class MoveAction : ActionNode
{
    public Transform Target;
    public Vector3 destination;
    public Vector3 startPosition;
    public float moveTime = 2f;
    private float elapsedTime;
    public override NodeState Evaluate()
    {
        if (isStarted == false)
        {
            OnStart();
            isStarted = true;
        }

        Vector2 newVec = Vector2.Lerp(Target.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Time.deltaTime);
        Debug.Log(newVec); 
        Target.position = newVec;

        return state = NodeState.Running;
    }

    public override void OnStart()
    {
        startPosition = Target.position;
        destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public override void Abort()
    {
        Debug.Log("Move Abort");
        OnEnd();
    }

    public override void OnEnd()
    {
        elapsedTime = 0f;
        isStarted = false;
        Target.position = startPosition;
    }
}
