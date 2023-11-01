using UnityEngine;

namespace RPG.Control
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector3 MoveInput { get; private set; }
        public bool IsInput => MoveInput != Vector3.zero;

        public void UpdateInputValue(Vector2 input)
        {
            MoveInput = new Vector3(input.x, 0, input.y);
        }
    }
}
