using RPG.Combat.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : ActiveSkill
{
    public TestCode prefab;

    public BasicAttack()
    {
        prefab = Resources.Load<TestCode>("Test");
    }

    public override void Activate(Transform initiator)
    {
        var cl = MonoBehaviour.Instantiate(prefab);
        GameObject player = GameObject.FindWithTag("Player");
        cl.Shoot(player.transform.position, player.transform.forward, 10);
    }
}

public class TestCode : MonoBehaviour
{
    public float speed;
    public float live;
    public float elapsedTime;
    public Vector3 dir;
    
    public void Shoot(Vector3 start, Vector3 dir, float speed)
    {
        live = 2f;
        transform.position= start;
        this.dir = dir;
        this.speed = speed;
    }

    private void Update()
    {
        transform.position += dir * Time.deltaTime * speed;
        elapsedTime += Time.deltaTime;
        if(elapsedTime > live) 
        {
            Destroy(gameObject);
            return;
        }
    }
}

