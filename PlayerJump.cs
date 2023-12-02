using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [Header("Jump Details")]    
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    public bool stoppedJumping;
    
    [Header("Ground Details")]
    [SerializeField] private Transform groundCheck1;
    [SerializeField] private Transform groundCheck2;
    [SerializeField] private float radOCircle;
    [SerializeField] private LayerMask ground;
    public bool grounded1;
    public bool grounded2;

    [Header("Ground Details")]
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        jumpTimeCounter = jumpTime;
    }

    private void Update() {
        //player is touching the ground
        grounded1 = Physics2D.OverlapCircle(groundCheck1.position, radOCircle, ground);
        grounded2 = Physics2D.OverlapCircle(groundCheck2.position, radOCircle, ground);

        if(grounded1 || grounded2) {
            jumpTimeCounter = jumpTime;
        }
        // jump press
        if (Input.GetButtonDown("Jump") && (grounded1 || grounded2)) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            stoppedJumping = false;
        }
        //jump hold
        if (Input.GetButton("Jump") && !stoppedJumping && (jumpTimeCounter > 0)) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }
        //jump release
        if (Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(groundCheck1.position, radOCircle);
        Gizmos.DrawSphere(groundCheck2.position, radOCircle);
    }
}
