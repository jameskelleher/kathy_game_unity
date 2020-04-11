using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FountainController : MonoBehaviour {
    bool playerInTrigger;
    Animator animator;

    void Start () {
        playerInTrigger = false;
        animator = this.GetComponent<Animator> ();
    }
    void Update () {
        if (playerInTrigger && Input.GetKeyDown ("right shift")) {
            animator.Play ("fountain_sparkle", 0);
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (!other.gameObject.GetComponent<PlayerController> ()) {
            return;
        }

        PhotonView pv = other.GetComponent<PhotonView> ();
        if (!pv.IsMine && PhotonNetwork.IsConnected) {
            return;
        }

        playerInTrigger = true;
    }

    void OnTriggerExit2D (Collider2D other) {
        if (!other.gameObject.GetComponent<PlayerController> ()) {
            return;
        }

        PhotonView pv = other.GetComponent<PhotonView> ();
        if (!pv.IsMine && PhotonNetwork.IsConnected) {
            return;
        }

        playerInTrigger = false;
    }
}