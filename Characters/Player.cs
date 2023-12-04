using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float walkSpeed = 1.0f;
    private float runSpeed = 2.0f;

    public override void Start() {
        base.Start();
    }

    public override void Update() {
        base.Update();
        direction = Input.GetAxisRaw("Horizontal");
        HandleJumping();
    }

    protected override void HandleMovement() {
        base.HandleMovement();
        myAnimator.SetFloat("speed", Mathf.Abs(direction));
        TurnAround(direction);
    }
    protected override void HandleJumping() {
        if(grounded) {
            jumpTimeCounter = jumpTime;
            myAnimator.ResetTrigger("jump");
            myAnimator.SetBool("falling", false);
        }
        if (Input.GetButtonDown("Jump") && (grounded)) {
            stoppedJumping = false;
            myAnimator.SetTrigger("jump");
            Jump();
        }
        //jump hold
        if (Input.GetButton("Jump") && !stoppedJumping && (jumpTimeCounter > 0)) {
            Jump();
            jumpTimeCounter -= Time.deltaTime;
            myAnimator.SetTrigger("jump");
        }
        //jump release
        if (Input.GetButtonUp("Jump"))
        {
            myAnimator.ResetTrigger("jump");
            jumpTimeCounter = 0;
            stoppedJumping = true;
            myAnimator.SetBool("falling", true);
        } 
    }
}
