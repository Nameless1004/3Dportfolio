using RPG.Core.Manager;
using RPG.Core.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    string GetSceneName(SceneType type)
    {
        string name = Enum.GetName(typeof(SceneType), type);
        return name;
    }

    public void LoadScene(SceneType type)
    {
        Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    public void Clear()
    {
        if(CurrentScene != null)
        {
            CurrentScene.Clear();
        }
    }
}
