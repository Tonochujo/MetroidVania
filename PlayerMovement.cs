using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    //necessary for animations and physics
    private Rigidbody2D rb2D;
    private Animator myAnimator;

    //variables to play with
    public float speed = 2.0f;
    public float horizontalMove; // 1, -1, 0 

    // Start is called before the first frame update
    private void Start() {
        //Define the game objects found on the player
        rb2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Handles input for the physics
    private void Update() {
        //check if the player has input movements
        horizontalMove = Input.GetAxis("Horizontal");

        // vvvv Instant Turn Around vvvv
        // horizontalMove = Input.GetAxisRaw("Horizontal");

    }
    //Handles running the physics
    private void FixedUpdate() {
        //move the player left right
        rb2D.velocity = new Vector2(horizontalMove*speed,rb2D.velocity.y);
    }
}
