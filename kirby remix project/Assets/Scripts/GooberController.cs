using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GooberController : MonoBehaviour
{
    
    
    Rigidbody2D gooberRig;
    Animator animator; //for goober's regular animation
    AudioSource audioSource;

    // Projectiles
    //public GameObject projectilePrefab;
    [SerializeField] private Rigidbody2D projectilePrefab;
    [SerializeField] private float projSpeed = 15f;
    private Rigidbody2D projectileRB;

    [SerializeField] private Transform groundCheck; //checks collision with ground to prevent infinite jumping.
    [SerializeField] private LayerMask groundLayer;

    public float jumpingPower = 16f; //declaring various movement variables

    private float horizontal;
    private bool isFacingRight = true;
    private bool doubleJump;

    public ParticleSystem absorbEffect; //absorb and hit particles
    public ParticleSystem hitEffect;

    public int health { get { return currentHealth; }} //goober's health
    public int currentHealth;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    Vector2 lookDirection = new Vector2(1,0);


    //Speed variable is for regular goob, Fspeed is for fire goob.
    public float speed = 8f; 
    public float Fspeed = 3f;

    // Dash speed
    public float Dspeed = 20f;
    public float dashDuration;

    // Booleans for each goob transformation.
    bool NormalGoob;
    bool FireGoob;
    bool WindGoob;

    // Booleans for Dashing
    bool isDashing;

    // Sprite Change during Absorb Mechanic
    public SpriteRenderer spriteR;
    public Sprite NormalGoober;  
    public Sprite FireGoober;
    public Sprite WindGoober;

    //God Mode
    public bool GodModeOn;
    
    // Start is called before the first frame update
    void Start()
    {
        gooberRig = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteR = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        
        //goob always starts the game as  Normalgoob.
        NormalGoob = true;
        FireGoob = false;
        WindGoob = false;

        dashDuration = 0;
        isDashing = false;

        GodModeOn = false;
    }

    void Update()
    {
        // Goober's Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        Vector2 move = new Vector2(horizontal, 0);
            
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        Flip();

        if (IsGrounded())
        {
            doubleJump = false;
        }

        // Jump Mechanic 
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

        if(GodModeOn)
        {
            currentHealth = 1;
        }

        if(isDashing)
        {
            dashDuration += Time.deltaTime;
            if(dashDuration >= 5)
            {
                speed = 3.0f;
                dashDuration = 0;
                isDashing = false;
            }
        }

        // Dash Mechanic
        if(Input.GetKeyDown(KeyCode.R))
        {   
            isDashing = true;
            speed = Dspeed;
        }

        // Absorb Mechanic
        if(Input.GetKeyDown(KeyCode.E))
        { 
            Absorb();
            
            print("Fire mode");         
        }
        
      
    }

    void Absorb()
    {
        Launch();
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        Destroy(enemy);
        
    }

    private void FixedUpdate()
    {

        /*This is the big one. These are the functions that will hold most of the changes between the goober transformations. 
        Put all the animations, projectile functions in here. Also you might want to add a third jump into this part 
        for the air goob or if it doesn't work add the third jump onto the getkeydown space function above this fixedupdate. 
        Refer to the youtube tutorial for double jumping. */
        if (NormalGoob && !FireGoob && !WindGoob)
        {
            if(spriteR.sprite != FireGoober && spriteR.sprite != WindGoober) //
            {
                spriteR.sprite = NormalGoober;
            }
            gooberRig.velocity = new Vector2(horizontal * speed, gooberRig.velocity.y);
        }
        else if (FireGoob && !NormalGoob && !WindGoob)
        {
            gooberRig.AddForce(new Vector2(horizontal * Fspeed * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);;
            if(spriteR.sprite != NormalGoober && spriteR.sprite != WindGoober)
            {
                spriteR.sprite = FireGoober;
            }
        }
        else if (WindGoob && !FireGoob && !NormalGoob)
        {
            if(spriteR.sprite != NormalGoober && spriteR.sprite != FireGoober)
            {
                spriteR.sprite = WindGoober;
            }
            gooberRig.velocity = new Vector2(horizontal * speed, gooberRig.velocity.y);
            gooberRig.AddForce(new Vector2(gooberRig.velocity.x, jumpingPower)); 
        }
        
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name.Equals("Fire_Enemy"))
        { 
            NormalGoob = false;
            FireGoob = true;
            WindGoob = false;
        }
        else if(other.gameObject.name.Equals("Wind_Enemy"))
        {
            NormalGoob = false;
            FireGoob = false;
            WindGoob = true;
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
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        //UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);  

        if(currentHealth <= 0)
        {
            isInvincible = false;
            LoseState L = GetComponent<LoseState>();
            L.GameOver();
        }               
    }

    void Launch()
    {
            
        projectileRB = Instantiate(projectilePrefab, gooberRig.position + Vector2.up * 0.5f, Quaternion.identity);
        projectileRB.velocity = projectileRB.transform.right * projSpeed;
           
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

    
}


