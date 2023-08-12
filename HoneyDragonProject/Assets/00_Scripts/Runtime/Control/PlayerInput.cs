using UnityEngine;

namespace RPG.Control
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector3 MoveInput { get; private set; }

        private void Update()
        {
            float z = Input.GetAxisRaw("Vertical");
            float x = Input.GetAxisRaw("Horizontal");
            MoveInput = new Vector3(x, 0, z);
        }
    }
}
