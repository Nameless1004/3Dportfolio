using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTest : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float lifeTime;
    private float damage;

    float elapsedTime;


    private void Update()
    {
        if(elapsedTime > lifeTime) Destroy(gameObject);

        transform.Translate(direction * speed * Time.deltaTime);
        elapsedTime += Time.deltaTime;
    }

    public void SetProjectile(Vector3 shootPosition, Vector3 dir, float speed, float lifeTime, float damage)
    {
        transform.position = shootPosition;
        direction = dir;
        this.speed = speed;
        this.lifeTime = lifeTime;
        elapsedTime = 0;
    }
}
