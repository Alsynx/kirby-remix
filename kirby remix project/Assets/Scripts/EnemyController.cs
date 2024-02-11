using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed; // speed var: how fast enemy moves --Asha
    public Rigidbody2D enemyRig; // var to control enemy's rigidbody --Asha
    Animator animator; // var accesses enemy's animator --Asha

    private float timer;
    public float changeTime = 3.0f;
    int direction = 1;

    Vector2 lookDirection = new Vector2(1, 0);

    private GooberController gooberController;

    private GameObject player;

    // test code
    [SerializeField] private Rigidbody2D enemyProjPrefab;
    [SerializeField] private float timeBtwAttacks = 2f;
    [SerializeField] private float projSpeed = 15f;
    private float attackTimer;
    private Rigidbody2D enemyProjectileRB;

    // Use this for initialization
    void Start()
    {
        
        GameObject gooberControllerObject = GameObject.FindWithTag("Player"); //this line of code finds the GooberController script by looking for a "Player" tag on Goober

        if (gooberControllerObject != null)
        {

            gooberController = gooberControllerObject.GetComponent<GooberController>(); //and this line of code finds the rubyController and then stores it in a variable

            print("Found the GooberController Script!");

        }

        if (gooberController == null)
        {

            print("Cannot find GameController Script!");

        }

        player = GameObject.FindGameObjectWithTag("Player"); 
        enemyRig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < 10)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= timeBtwAttacks)
            {
                attackTimer = 0;
                AttackGoober();
            }
        }
        
    }

    void FixedUpdate()
    {
        
        Vector2 position = enemyRig.position;
        
        position.x = position.x + Time.deltaTime * speed * direction;
        //animator.SetFloat("Move X", direction);
        //animator.SetFloat("Move Y", 0);
        
        enemyRig.MovePosition(position);
    }
    
    // method allows enemies to attack Goober
    void AttackGoober()
    {
        enemyProjectileRB = Instantiate(enemyProjPrefab, enemyRig.position + Vector2.up * 0.5f, Quaternion.identity);
        enemyProjectileRB.transform.right = GetAttackDirection();
        enemyProjectileRB.velocity = enemyProjectileRB.transform.right * projSpeed;
    }

    // this method insures the attack follows Goober base on his movement
    public Vector2 GetAttackDirection()
    {
        Transform playerTrans = GameObject.FindGameObjectWithTag("Player").transform; 
        return (playerTrans.position - transform.position).normalized;
    }
    
    public void EnemyDeath()
    {
        if (gooberController != null)
        {
            
            enemyRig.simulated = false;
            //optional if you added the fixed animation
            //animator.SetTrigger("EnemyDeath"); <----- ALFRED
            Destroy(gameObject);
        }
    } 
}
