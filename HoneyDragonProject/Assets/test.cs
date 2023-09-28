using RPG.Control;
using RPG.Core.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    public PlayerSkillController skillController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            skillController =FindAnyObjectByType<PlayerSkillController>();
            skillController.AddSkill(2001, 1);
        }
    }

    public void cc()
    {
        Managers.Instance.Game.CreatePlayer();
    }
}
