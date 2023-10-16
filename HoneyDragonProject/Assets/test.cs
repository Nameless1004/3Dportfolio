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
        float currentVelocity = 0f;
        var targetAngle = Mathf.Atan2(0, 0) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, 0.1f);
        Quaternion.Euler(0, angle, 0);
    }

}
