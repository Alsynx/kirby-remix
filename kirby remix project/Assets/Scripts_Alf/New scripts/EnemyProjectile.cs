using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float timer; 

    public ParticleSystem hitEffect;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 10)
        {
            Destroy(gameObject); // after ten secs the enemyProjPrefab disappeears
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GooberController controller = other.GetComponent<GooberController>();

        if (controller != null)
        {
            controller.HealthyGoober(-1); // if the play gets hit than they lose heath
            Destroy(gameObject); // after Goober gets hit the enemyProjPrefab disappeears
            Instantiate(hitEffect, transform.position, transform.rotation);
        }
    }
}