using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float bulletSpeed;
    private Rigidbody2D bulletRigidBody;

    // Use this for initialization
    void Start () {
        bulletRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        bulletRigidBody.velocity = new Vector2(bulletSpeed, bulletRigidBody.velocity.y);
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Destroy(gameObject);

        if(collider.gameObject.tag == "Player") {
            Physics2D.IgnoreCollision(collider.GetComponent<Collider2D>(), collider);
        }
    }
}
