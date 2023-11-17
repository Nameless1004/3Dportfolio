using RPG.Combat.AI.BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Control
{
    public class PetAIController : MonoBehaviour
    {
        [SerializeField] BehaviourTreeRunner behaviourTreeRunner;
        public GameObject Player;
        
        void Start()
        {
            behaviourTreeRunner.Run();
        }


        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
