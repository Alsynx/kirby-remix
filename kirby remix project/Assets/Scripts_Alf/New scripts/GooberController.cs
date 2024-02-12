using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GooberController : MonoBehaviour
{
    
    public GameObject absorbHitbox;

    public GameObject Goober;
    public GameObject Lose_Screen;
    public AudioClip YouLoseSound;
    
    Rigidbody2D gooberRig;
    Animator animator; //for goober's regular animation
    public AudioSource audioSource;

    // Projectiles
    //public GameObject projectilePrefab;
    [SerializeField] private Rigidbody2D projectileFirePrefab;
    [SerializeField] private Rigidbody2D projectileWindPrefab;
    [SerializeField] private float projSpeed = 15f;
    private Rigidbody2D projectileRB;

    [SerializeField] private Transform groundCheck; //checks collision with ground to prevent infinite jumping.
    [SerializeField] private LayerMask groundLayer;

    public float jumpingPower = 16f; //declaring various movement variables

    private float horizontal;
    private bool isFacingRight = true;
    private bool doubleJump;

    public ParticleSystem absorbEffect; //absorb and hit particles
    //public ParticleSystem hitEffect;

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

    public AnimatorOverrideController greenAnim;
    public AnimatorOverrideController redAnim; //to change skins -Alfred

    //God Mode
    public bool oneShot;
    public bool cheatMode;
    
    // Start is called before the first frame update
    void Start()
    {
        gooberRig = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
        spriteR = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        
        //goob always starts the game as  Normalgoob.
        NormalGoob = true;
        FireGoob = false;
        WindGoob = false;

        dashDuration = 0;
        isDashing = false;

        oneShot = false;
        cheatMode = false;
    }

    void Update()
    {
        // Goober's Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        Vector2 move = new Vector2(horizontal, 0);

        animator.SetFloat("Speed", Mathf.Abs(horizontal)); //added to allow animator to, well, animate -Alfred
            
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        Flip();

        if (IsGrounded())
        {
            doubleJump = false;
            animator.SetBool("IsJumping", false); //jump anim -Alfred
            animator.SetBool("IsGrounded", true);
        }

        // Jump Mechanic 
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJumping", true); //jump anim -Alfred
            animator.SetBool("IsGrounded", false);

            if (IsGrounded())
            {
                gooberRig.velocity = new Vector2(gooberRig.velocity.x, jumpingPower);
            }
        
            else if (!doubleJump)
            {
                gooberRig.velocity = new Vector2(gooberRig.velocity.x, jumpingPower);
                doubleJump = true;
            }

        }

        if(cheatMode)
        {
            isInvincible = true;
        }

        if(oneShot)
        {
            if (isInvincible)
            {
                invincibleTimer -= Time.deltaTime;
                if (invincibleTimer < 0)
                    isInvincible = false;
            }

            currentHealth = 1;

            if(currentHealth <= 0)
            {
                isInvincible = false;
                GameOver();
            }   
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
        /*Instantiate(absorbEffect, transform.position, transform.rotation); //added to play particles on absorbing -Alfred
        Destroy(absorbEffect);*/
    }

    private void FixedUpdate()
    {

        /*This is the big one. These are the functions that will hold most of the changes between the goober transformations. 
        Put all the animations, projectile functions in here. Also you might want to add a third jump into this part 
        for the air goob or if it doesn't work add the third jump onto the getkeydown space function above this fixedupdate. 
        Refer to the youtube tutorial for double jumping. */
        if (NormalGoob && !FireGoob && !WindGoob)
        {
            if(spriteR.sprite != FireGoob && spriteR.sprite != WindGoob) //
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
                GetComponent<Animator>().runtimeAnimatorController = redAnim as RuntimeAnimatorController;
                Debug.Log("Fire get!");
            }
        }
        else if (WindGoob && !FireGoob && !NormalGoob)
        {
            if(spriteR.sprite != NormalGoober && spriteR.sprite != FireGoober)
            {
                spriteR.sprite = WindGoober;
                GetComponent<Animator>().runtimeAnimatorController = greenAnim as RuntimeAnimatorController;
                Debug.Log("Wind get!");
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
            EnemyController controller = other.GetComponent<EnemyController>();
            if (controller !=null)
            {
                controller.enemyAbsorbed = true;
            }

            if(other.gameObject.name.Equals("Fire_Enemy"))
            { 
                NormalGoob = false;
                FireGoob = true;
                WindGoob = false;
                Instantiate(absorbEffect, transform.position, transform.rotation);
            }
            else if(other.gameObject.name.Equals("Wind_Enemy"))
            {
                NormalGoob = false;
                FireGoob = false;
                WindGoob = true;
                Instantiate(absorbEffect, transform.position, transform.rotation);
            }

         //added to play particles on absorbing -Alfred
        Destroy(absorbEffect);
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
        
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);  

        if(currentHealth <= 0)
        {
            isInvincible = false;
            //LoseState lose = GetComponent<LoseState>();
            GameOver();


        }               
    }

    public void GameOver()
    {
        
        Lose_Screen.SetActive(true);  //activates the lose screen when goober falls on the collider.
        PlaySound(YouLoseSound);
        Goober.SetActive(false); //deletes goober.
    }

    void Launch()
    {
        if (!NormalGoob)
        {
            if(FireGoob && !WindGoob)
            {
                projectileRB = Instantiate(projectileFirePrefab, gooberRig.position + Vector2.up * 0.5f, Quaternion.identity);
                projectileRB.velocity = projectileRB.transform.right * projSpeed;
            }
            else if (WindGoob && !FireGoob)
            {
                projectileRB = Instantiate(projectileWindPrefab, gooberRig.position + Vector2.up * 0.5f, Quaternion.identity);
                projectileRB.velocity = projectileRB.transform.right * projSpeed;
            }
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

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}


