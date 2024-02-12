using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    private float timer; 

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

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
        EnemyController e = other.GetComponent<EnemyController>();
        if (e != null)
        {
            e.EnemyDeath();
            Destroy(gameObject);
        }
    
    }
}
