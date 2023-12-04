using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour {

    [Header("Movement Variables")]
    [SerializeField] protected float speed = 1.0f;
    [SerializeField] protected float direction;

    protected bool facingRight = true;

    [Header("Jump Variables")]
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float jumpTime;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float radOCircle;
    [SerializeField] protected LayerMask ground;
    [SerializeField] protected bool grounded;
    protected float jumpTimeCounter;
    protected bool stoppedJumping;

    [Header("Attack Variables")]

    [Header("Character Stats")]

    private Rigidbody2D rb2D;
    public Animator myAnimator;

    #region monos
    public virtual void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
    }
    public virtual void Update() {
        //handle user input
        grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, ground);
        if (rb2D.velocity.y < 0){
            myAnimator.SetBool("falling", true);
        }

    }
    public virtual void FixedUpdate() {
        //handle mechanics/submechanics
        HandleMovement();
        HandleLayers();
    }
    #endregion

    protected void Jump() {
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
    }
    protected abstract void HandleJumping();

    protected virtual void HandleMovement() {
        Move();
    }

    protected void Move() {
        rb2D.velocity = new Vector2(direction*speed, rb2D.velocity.y);
    }

    #region subMechanics
    protected void HandleLayers() {
        if (!grounded) {
         myAnimator.SetLayerWeight(1,1);
        } else {
         myAnimator.SetLayerWeight(1,0);
        }
    }
    protected void TurnAround (float horizontal) {
        if ((horizontal < 0 && facingRight || horizontal > 0 && !facingRight) && !Input.GetButton("Jump")) {
          facingRight = !facingRight;
          transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }
    protected void OnDrawGizmos() {
        Gizmos.DrawSphere(groundCheck.position, radOCircle);
    }
    #endregion
}
