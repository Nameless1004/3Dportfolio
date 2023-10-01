using Cysharp.Threading.Tasks;
using RPG.Core.Manager;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Vector2Int GridSize;
    public float CellRadius = 0.5f;
    public FlowField CurFlowField;
    private GridDebug gridDebug;

    private void Awake()
    {
        gridDebug = GetComponent<GridDebug>();
    }

    private void Start()
    {
        Test().Forget();
    }

    private void InitializedFlowField()
    {
        CurFlowField = new FlowField(CellRadius, GridSize);
        CurFlowField.CreateGrid();
        gridDebug.SetFlowField(CurFlowField);
    }


    private void UpdateField(Vector3 targetPos)
    {
        CurFlowField.ResetBestCost();
        CurFlowField.CreateCostField();
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(worldMousePos);
        Cell destinationCell = CurFlowField.GetCellFromWorldPos(targetPos);
        Debug.Log(targetPos);
        Debug.Log(destinationCell == null);
        CurFlowField.CreateIntegrationField(destinationCell);
        CurFlowField.CreateFlowField();
    }

    async UniTaskVoid Test()
    {
        InitializedFlowField();
        while (true)
        {
            if(Managers.Instance.Game.CurrentPlayer != null)
            {
                InitializedFlowField();
                UpdateField(Managers.Instance.Game.CurrentPlayer.position);
            }
            await UniTask.Delay(100, false, PlayerLoopTiming.EarlyUpdate, this.GetCancellationTokenOnDestroy());
        }
    }
}
