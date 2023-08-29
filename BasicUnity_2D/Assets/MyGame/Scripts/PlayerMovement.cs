using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;



    [SerializeField] private LayerMask jumableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling, attack, death }

  
    MovementState state;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
           
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.gravityScale = 3;
           
        }

        if (IsGrounded())
        {
            anim.SetBool("isGrounded", true);
        } else if  (!IsGrounded())
        {
            anim.SetBool("isGrounded", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (state == MovementState.running)
            {
                anim.SetTrigger("isSlide");
            }

        }

        if (Input.GetMouseButton(0))
        {
            anim.SetTrigger("isAtk");
        }


    }

    private void FixedUpdate()
    {
        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {


        state = MovementState.idle;

        if (rb.velocity.x > 1f)
        {
            sprite.flipX = false;
            state = MovementState.running;
        } else if (rb.velocity.x < 0f)
        {
            sprite.flipX = true;
            state = MovementState.running;
        } else
        {
          
        }


        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }
   

    private void Die()
    {
        anim.SetTrigger("death");
    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumableGround);
    }


}