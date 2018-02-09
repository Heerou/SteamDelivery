using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private CharacterController characterController;
    private bool isGrounded;
    public float gravity;
    private float fallSpeed;
    public float jumpForce;
    public float moveSpeed;
    //Shotting
    public Transform bulletEmitter;
    public GameObject bullet;

    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        IsGrounded();
        Fall();
        Jump();
        PlayerMove();
        ShootingBullets();

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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            fallSpeed = -jumpForce;
        }
    }

    void IsGrounded() {
        //With raycast i check is the player is touching the ground
        isGrounded = Physics.Raycast(transform.position, -transform.up, characterController.height / 1.8f);
    }

    void ShootingBullets() {
        if (Input.GetKeyDown(KeyCode.X)) {
            Instantiate(bullet, bulletEmitter.position, bulletEmitter.rotation);
        }
    }
}
