using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    //Getting the RigidBody
    private Rigidbody2D characterRigidBody;

    //Speed of the player
    public float maxSpeed = 10f;
    bool facingRight = true;

    //Jump of the player
    public float jumpForce = 8f;    

    //Ground
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround = 9;    

    // Use this for initialization
    void Start () {
        characterRigidBody = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void FixedUpdate() {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    }

    void Update() {

        float move = Input.GetAxis("Horizontal");
        characterRigidBody.velocity = new Vector2(move * maxSpeed, characterRigidBody.velocity.y);

        if (move > 0 && !facingRight) {
            Flip();
        } else if (move < 0 && facingRight) {
            Flip();
        }

        if(Input.GetKeyDown(KeyCode.Space) && grounded) {            
            characterRigidBody.velocity = new Vector2(characterRigidBody.velocity.x, jumpForce);
        }
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
