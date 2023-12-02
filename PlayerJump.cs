using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerJump : MonoBehaviour
{

    [Header("Jump Details")]    
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    public bool stoppedJumping;
    
    [Header("Ground Details")]
    [SerializeField] private Transform groundCheck1;
    [SerializeField] private float radOCircle;
    [SerializeField] private LayerMask ground;
    public bool grounded1;
    

    [Header("Ground Details")]
    private Rigidbody2D rb;
    private Animator myAnimator;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
    }

    private void Update() {
        //player is touching the ground
        grounded1 = Physics2D.OverlapCircle(groundCheck1.position, radOCircle, ground);

        if(grounded1) {
            jumpTimeCounter = jumpTime;
            myAnimator.SetBool("falling", false);
            myAnimator.ResetTrigger("jump");
        }
        // jump press
        if (Input.GetButtonDown("Jump") && (grounded1)) {
            myAnimator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            stoppedJumping = false;
        }
        //jump hold
        if (Input.GetButton("Jump") && !stoppedJumping && (jumpTimeCounter > 0)) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
            myAnimator.SetTrigger("jump");
        }
        //jump release
        if (Input.GetButtonUp("Jump"))
        {
            myAnimator.ResetTrigger("jump");
            jumpTimeCounter = 0;
            stoppedJumping = true;
            
        }
        if (rb.velocity.y < 0)
        {
            myAnimator.SetBool("falling", true);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(groundCheck1.position, radOCircle);
    }
    private void FixedUpdate() {
        HandleLayers();
    }

    private void HandleLayers() {
        if (!grounded1) {
            myAnimator.SetLayerWeight(1,1);
        } else {
            myAnimator.SetLayerWeight(1,0);
        }
    }
}
