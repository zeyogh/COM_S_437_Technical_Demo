using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D collider;
    [SerializeField] private LayerMask jumpableGround;

    //private Animator anim;
    //private SpriteRenderer sprite;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    private enum MovementState {idle, running, jumping, falling};

    [SerializeField] private AudioSource jumpSoundEffect;

    //private MovementState state = MovementState.idle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        //sprite = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal"); //raw to make it stop as soon as you release the button, only need this in Update
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); //rb.velocity,y is the value the y velocity had the frame before, to not drop it to zero

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            //jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            //sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            //anim.SetBool("running", true);
            //sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
            //anim.SetBool("running", false);
        }

        if (rb.velocity.y > .1f) //velocity imprecision
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        //anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

}
