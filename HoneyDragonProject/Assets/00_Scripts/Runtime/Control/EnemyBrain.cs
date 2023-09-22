using RPG.Core;
using UnityEngine;

namespace RPG.Control
{
    public class EnemyBrain : MonoBehaviour
    {
        public Player Target { get; set; }

        private void Awake()
        {
            Target = FindObjectOfType<Player>();
        }
    }
}
