using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

  public enum Elements
    {
        Fire,
        Air
    };


public class EnemyOld : MonoBehaviour
{
    
    /* 
        URGENT PLEASE READ  
        in case of code rewrites or additions please sign your name in note comments
        Thank You --Asha 

        ***Extra Note delete later***
        Hey Carlos I just gave you the skeleton and the variables if you follow the 
        EnemyController on Ruby this should be easy. Just get rid of the "verticle code"
        cause our enemies won't need to walk verticle. --Asha
    */

    public Elements element;

    public float speed; // speed var: how fast enemy moves --Asha
    public Rigidbody2D enemyRig; // var to control enemy's rigidbody --Asha
    Animator animator; // var accesses enemy's animator --Asha

    public Sprite mySprite;

    public GameObject Projectile;

    private float timer;

    Vector2 lookDirection = new Vector2(1, 0);

    private GooberController gooberController;

      private GameObject player;

    // Use this for initialization
    void Start()
    {
        
        element= Elements.Fire;

        player = GameObject.FindGameObjectWithTag("Player"); 

       enemyRig = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
       mySprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy attacks Goober when in range
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < 10)
        {
            // Enemy attacks Goober every 2 seconds
            timer += Time.deltaTime;
            if(timer > 2)
            {
                timer = 0;
                Launch();
            }
        }

    }

    // if Goober touches enemy Goober loses health
    void OnCollisionEnter2D(Collision2D other)
    {


       /* this does not work because player gameobject does not have a script on.

        if (player != null)
        {
            player.HealthyGoober(-1);
        }*/
        
    }
    
    // method allows enemies to attack Goober
    void Launch()
    {

    }



    /* commented out because goober script is not working yet.
    Elements GooberChange (Elements ele)
    {
        switch(ele)
        {
            case Elements.Fire:
                player.mySprite();
                break;
            case Elements.Air:
                player.mySprite();
                break;
        } 
    }
    */
    /* commented out while we debug.
    public void EnemyDeath()
    {
            //optional if you added the fixed animation
            animator.SetTrigger("EnemyDeath");

            Destroy(enemies.gameObject);

            GooberController.ChangeScore(1);
    } */

}