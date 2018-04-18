using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour {

    public int Damage;
    public string TagToFind;

    Rigidbody bulletRB;
    public float Speed;

    // Use this for initialization
    void Awake() {
        bulletRB = GetComponent<Rigidbody>();
    }

    //private void OnTriggerEnter(Collider other) {
    //    if (other.CompareTag(TagToFind)) {
    //        //other.GetComponent<LifeComponent>().TakeDamage(Damage);
    //        SetState(false);
    //    }
    //}

    void SetState(bool state) {
        gameObject.SetActive(state);
    }

    public void Shoot() {
        bulletRB.velocity = transform.forward * Speed;
    }
}
