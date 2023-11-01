using UnityEngine;

namespace RPG.Control
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector3 MoveInput { get; private set; }
        public bool IsInput => MoveInput != Vector3.zero;

        private void Update()
        {
#if UNITY_EDITOR
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            MoveInput = new Vector3(x, 0, y).normalized;
#endif
        }
        public void UpdateInputValue(Vector2 input)
        {
            MoveInput = new Vector3(input.x, 0, input.y);
        }
    }
}
