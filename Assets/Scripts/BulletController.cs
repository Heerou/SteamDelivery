using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float bulletSpeed;
    private Rigidbody bulletRigidBody;

    // Use this for initialization
    void Start () {
        bulletRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        bulletRigidBody.velocity = new Vector3(bulletSpeed, bulletRigidBody.velocity.y, 0);
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter(Collider collider) {
        Destroy(gameObject);

        if(collider.gameObject.tag == "Player") {
            Physics.IgnoreCollision(collider.GetComponent<CharacterController>(), collider);
        }
    }
}
