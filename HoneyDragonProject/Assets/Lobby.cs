using RPG.Core.Manager;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField]
    Button myButton;

    private void Awake()
    {

    }

    public void StartGame()
    {
        Managers.Instance.Scene.LoadScene(RPG.Core.Scene.SceneType.Game);
    }
}
