using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyingBullets : MonoBehaviour {

    void OnEnable() {
        Invoke("Destroy", 2f);
    }

    void Destroy() {
        gameObject.SetActive(false);
    }

    //If the bullet is inable, to prevent issues
    void OnDisable() {
        CancelInvoke();
    }
}
