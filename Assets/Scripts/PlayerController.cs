using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private CharacterController characterController;
    private bool isGrounded;
    public float Gravity;
    private float gravityStore;
    private float fallSpeed;
    public float JumpForce;
    public float MoveSpeed;
    //Shotting
    public Transform BulletEmitterFront;
    public Transform BulletEmitterUp;
    public Transform BulletEmitterDiagonalUp;
    public Transform BulletEmitterDown;
    public Transform BulletEmitterDiagonalDown;
    GameObject FatherBullets;
    public GameObject Bullet;
    float fireRate = 0.5f;
    float nextFire = 0.5f;
    int currentBullet;
    float horziontal;
    float vertical;
    //Onladder
    public bool onLadder;
    public float climbSpeed;
    private float climbVelocity;

    public int PooledBullets = 5;
    List<GameObject> bullets;

    // Use this for initialization
    void Start() {
        characterController = GetComponent<CharacterController>();
        gravityStore = Gravity;

        FatherBullets = new GameObject("FatherBullets");
        bullets = new List<GameObject>();
        GameObject obj;
        for (int i = 0; i < PooledBullets; i++) {
            obj = (GameObject)Instantiate(Bullet);
            obj.SetActive(false);
            bullets.Add(obj);
            obj.transform.SetParent(FatherBullets.transform);
        }
    }

    // Update is called once per frame
    void Update() {
        IsGrounded();
        Fall();
        Jump();
        PlayerMove();
        Shooting();
        IntheLadder();

        horziontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void PlayerMove() {
        float xSpeed = Input.GetAxis("Horizontal");
        if (xSpeed != 0) {
            characterController.Move(new Vector3(xSpeed, 0, 0) * MoveSpeed * Time.deltaTime);
        }
        //Rotation of the character depending of the axis z
        if (xSpeed > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (xSpeed < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void Fall() {
        if (!isGrounded) {
            fallSpeed += Gravity * Time.deltaTime;
        } else {
            if (fallSpeed > 0)
                fallSpeed = 0;
        }
        characterController.Move(new Vector3(0, -fallSpeed) * Time.deltaTime);
    }

    void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            fallSpeed = -JumpForce;
        }
    }

    void IsGrounded() {
        //With raycast i check is the player is touching the ground
        isGrounded = Physics.Raycast(transform.position, -transform.up, characterController.height / 1.8f);
    }

    void Shooting() {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            if (horziontal >= 0 && vertical == 0) {
                bullets[currentBullet].transform.position = BulletEmitterFront.position;
                bullets[currentBullet].transform.rotation = BulletEmitterFront.rotation;
            } else if (vertical > 0 && horziontal == 0) {
                bullets[currentBullet].transform.position = BulletEmitterUp.position;
                bullets[currentBullet].transform.rotation = BulletEmitterUp.rotation;
            } else if (vertical < 0 && horziontal == 0) {
                bullets[currentBullet].transform.position = BulletEmitterDown.position;
                bullets[currentBullet].transform.rotation = BulletEmitterDown.rotation;
            } else if (vertical > 0 && horziontal > 0) {
                bullets[currentBullet].transform.position = BulletEmitterDiagonalUp.position;
                bullets[currentBullet].transform.rotation = BulletEmitterDiagonalUp.rotation;
            } else if (vertical > 0 && horziontal != 0) {
                bullets[currentBullet].transform.position = BulletEmitterDiagonalUp.position;
                bullets[currentBullet].transform.rotation = BulletEmitterDiagonalUp.rotation;
            } else if (vertical < 0 && horziontal != 0) {
                bullets[currentBullet].transform.position = BulletEmitterDiagonalDown.position;
                bullets[currentBullet].transform.rotation = BulletEmitterDiagonalDown.rotation;
            }
            bullets[currentBullet].GetComponent<Bullets>().Shoot();
            bullets[currentBullet].SetActive(true);
        }
        currentBullet++;
        if (currentBullet >= bullets.Count) {
            currentBullet = 0;
        }
    }

    void IntheLadder() {
        //When i touch the ladder i climb it
        if (onLadder) {
            Gravity = 0f;
            fallSpeed = 0f;
            climbVelocity = climbSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            characterController.Move(new Vector3(0, climbVelocity));
        } else {
            Gravity = gravityStore;
        }
    }
}
