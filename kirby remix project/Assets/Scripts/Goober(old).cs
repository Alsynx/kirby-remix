using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GooberOld : MonoBehaviour
{
    /* 
        URGENT PLEASE READ  
        in case of code rewrites or additions please sign your name in note comments
        Thank You --Asha 
     

    public float speed = 3f; // speed var: how fast Goober moves --Asha
   // public float jump = 333f;// // jumps var: how far Goober jumps --Asha

    bool isAbsorbed = false; // bool determines whether enemy has been absorbed --Asha

    Rigidbody2D gooberRig; // var to control Goober's rigidbody --Asha
    Animator animator; // var accesses Goober animator --Asha
    AudioSource audioSource; // var accesses AudioSource --Asha

    public ParticleSystem absorbEffect;
    public ParticleSystem hitEffect;

    bool isInvincible;
    float invincibleTimer;
    public float timeInvincible = 2.0f;

    public int health { get { return currentHealth; }}
    int currentHealth;
    public int maxHealth = 5;

    
    //placeholder for enemy rig while we debug//
    public GameObject e;


    // Use this for initialization
    void Start()
    {
        gooberRig = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if states that allow Goober to move left and right --Asha
        if (Input.GetKey (KeyCode.A)) 
        {
		    transform.Translate (Vector2.left * speed * Time.deltaTime); 
	    }

        if(Input.GetKey (KeyCode.D)) 
        {
            transform.Translate (Vector2.right * speed * Time.deltaTime);
        }

        // Jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //gooberRig.AddForce(Vector2.up * jump, ForceMode2D.Impulse)//;
            gooberRig.velocity = new Vector3(0, 14, 0);
        }

        // Run and Jump
       /* float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 run = new Vector2(horizontal, vertical);
        gooberRig.AddForce(run * 2f);

        // Absorb/Using Power --Asha
        if(Input.GetKeyDown(KeyCode.E))
        { 
            // ***Note for Carlos*** this code depends on public enum Elements from EnemyController --Asha
            Elements element;
            element = e.GetComponent<EnemyController>().element;
            animator.SetTrigger("Absorb");
            absorbEffect = Instantiate(absorbEffect, transform.position, Quaternion.identity);
            isAbsorbed = true;

            if(isAbsorbed = true)
            {
                // come back to this when projectile is ready !!  Launch(); //
            }      
        }

        // Health
        if (isInvincible)
            {
                invincibleTimer -= Time.deltaTime;
                if (invincibleTimer < 0)
                    isInvincible = false;
            }
    }

    public void HealthyGoober(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
                
            isInvincible = true;
            invincibleTimer = timeInvincible;
            // take damage effect
            animator.SetTrigger("Hit");
            hitEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                
           // commented out because we have no sounds yet. PlaySound(hitSound); //
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            
         commented out until we put in ui.
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth); 

        if(currentHealth <= 0)
        {
            isInvincible = false;
            /* Commented out until implementation.
            GameOver();
        }
    }
    
   commented out until projectile gameobject is implemented.
    void Launch()
    {
         GameObject projectileObject = Instantiate(projectilePrefab, gooberRig.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        
        PlaySound(throwSound);
    } */
}