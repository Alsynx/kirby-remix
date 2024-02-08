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
    public float speed =6f;
    private bool doubleJump;

    public ParticleSystem absorbEffect; //absorb and hit particles
    public ParticleSystem hitEffect;

    public int health { get { return currentHealth; }} //goober's health
    public int currentHealth;
    public int maxHealth = 5;

    
    // Start is called before the first frame update
    void Start()
    {
        gooberRig = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        //goober's movement
        horizontal = Input.GetAxisRaw("Horizontal");

        Flip();

        if (IsGrounded())
        {
            doubleJump = false;
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
        gooberRig.velocity = new Vector2(horizontal * speed, gooberRig.velocity.y);
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


