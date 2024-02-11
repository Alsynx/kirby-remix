using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooberController : MonoBehaviour
{
    
    
    Rigidbody2D gooberRig;
    Animator animator; //for goober's regular animation
    AudioSource audioSource;

    

    [SerializeField] private Transform groundCheck; //checks collision with ground to prevent infinite jumping.
    [SerializeField] private LayerMask groundLayer;

    public float jumpingPower = 16f; //declaring various movement variables

    private float horizontal;
    private bool isFacingRight = true;

    //Speed variable is for regular goob, Fspeed is for fire goob.
    public float speed =8f; 
    public float Fspeed =600f;

    private bool doubleJump;

    public ParticleSystem absorbEffect; //absorb and hit particles
    public ParticleSystem hitEffect;

    public int health { get { return currentHealth; }} //goober's health
    public int currentHealth;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
// Booleans for each goob transformation.
    bool NormalGoob;
    bool FireGoob;

    
    // Start is called before the first frame update
    void Start()
    {
        gooberRig = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;

        //goob always starts the game as  Normalgoob.
        NormalGoob = true;
        FireGoob = false;
    }

    void Update()
    {
        //goober's movement (NOT WORKING)
        horizontal = Input.GetAxisRaw("Horizontal");

        Flip();

        if (IsGrounded())
        {
            doubleJump = false;
        }

      //Test function of the Goob Transform System. WORKS!
      if (Input.GetKeyDown(KeyCode.X))
        {
             NormalGoob = false;
            FireGoob = true;
        print("Fire mode");
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {

                if (IsGrounded())
            gooberRig.velocity = new Vector2(gooberRig.velocity.x, jumpingPower);

            
         
        
            else if (!doubleJump)
            {
                gooberRig.velocity = new Vector2(gooberRig.velocity.x, jumpingPower);
                doubleJump = true;
               
            }


        }

      
    }
                bool IsGrounded()
            {
                    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
            }

    private void FixedUpdate()
    {
       /* gooberRig.velocity = new Vector2(horizontal * speed, gooberRig.velocity.y);*/

        /*This is the big one. These are the functions that will hold most of the changes between the goober transformations. Put all the animations, projectile functions in here. Also you might want to add a third jump into this part 
        for the air goob or if it doesn't work add the third jump onto the getkeydown space function above this fixedupdate. Refer to the youtube tutorial for double jumping. */

       if (NormalGoob && !FireGoob)
       {
       gooberRig.AddForce(new Vector2(horizontal * speed * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);
       }
              if (FireGoob && !NormalGoob)
       {
       gooberRig.AddForce(new Vector2(horizontal * Fspeed * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);
       }
       
    }

     private void Flip()
    {

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
             {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
             }
    }

        public void ChangeHealth(int amount) //function to change health
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    
}


