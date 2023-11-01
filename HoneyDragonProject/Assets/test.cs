using RPG.Control;
using RPG.Core.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Instance.Sound.PlaySound(SoundType.Effect, "Sound/test1");
        }

        if(Input.GetKeyDown(KeyCode.W)) 
        {
            Managers.Instance.Sound.PlaySound(SoundType.Effect, "Sound/test2");
        }
    }

}
