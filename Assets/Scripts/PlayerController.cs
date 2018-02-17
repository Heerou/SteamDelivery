using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private CharacterController characterController;
    private bool isGrounded;
    public float gravity;
    private float gravityStore;
    private float fallSpeed;
    public float jumpForce;
    public float moveSpeed;
    //Shotting
    public Transform bulletEmitter;
    public GameObject bullet;
    //Onladder
    public bool onLadder;
    public float climbSpeed;
    private float climbVelocity;

    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
        gravityStore = gravity;
    }
	
	// Update is called once per frame
	void Update () {
        IsGrounded();
        Fall();
        Jump();
        PlayerMove();
        ShootingBullets();
        IntheLadder();

    }

    void PlayerMove() {
        float xSpeed = Input.GetAxis("Horizontal");
        if(xSpeed != 0) {
            characterController.Move(new Vector3(xSpeed, 0, 0) * moveSpeed * Time.deltaTime);
        }
    }

    void Fall() {
        if (!isGrounded) {
            fallSpeed += gravity * Time.deltaTime;
        }
        else {
            if (fallSpeed > 0)
                fallSpeed = 0;
        }
        characterController.Move(new Vector3(0, -fallSpeed) * Time.deltaTime);
    }

    void Jump() {
        if (Input.GetButton("Jump") && isGrounded) {
            fallSpeed = -jumpForce;
        }
    }

    void IsGrounded() {
        //With raycast i check is the player is touching the ground
        isGrounded = Physics.Raycast(transform.position, -transform.up, characterController.height / 1.8f);
    }

    void ShootingBullets() {
        //just instantiate the bullet
        if (Input.GetButtonDown("Fire1")) {
            Instantiate(bullet, bulletEmitter.position, bulletEmitter.rotation);
        }
    }

    void IntheLadder() {
        //When i touch the ladder i climb it
        if (onLadder) {
            gravity = 0f;
            fallSpeed = 0f;
            climbVelocity = climbSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            characterController.Move(new Vector3(characterController.velocity.x, climbVelocity));
        } else {
            gravity = gravityStore;
        }
    }
}
