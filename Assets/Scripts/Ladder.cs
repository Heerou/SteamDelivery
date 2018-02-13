using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    private PlayerController ThePlayer;

	// Use this for initialization
	void Start () {
        ThePlayer = FindObjectOfType<PlayerController>();

    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && other.name == "Player") {
            ThePlayer.onLadder = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player" && other.name == "Player") {
            ThePlayer.onLadder = false;
        }
    }
}
