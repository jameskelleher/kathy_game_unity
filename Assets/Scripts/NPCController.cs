using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCController : MonoBehaviour {

    public GameObject choosePanel;

    bool playerInTrigger;

    // Update is called once per frame
    void Update () {
        this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.y);

        if (playerInTrigger && Input.GetKeyDown ("right shift")) {
            choosePanel.SetActive (true);
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (!other.CompareTag ("Player")) {
            return;
        }

        PhotonView pv = other.GetComponent<PhotonView> ();
        if (!pv.IsMine && PhotonNetwork.IsConnected) {
            return;
        }

        playerInTrigger = true;
    }

    void OnTriggerExit2D (Collider2D other) {
        if (!other.CompareTag ("Player")) {
            return;
        }

        PhotonView pv = other.GetComponent<PhotonView> ();
        if (!pv.IsMine && PhotonNetwork.IsConnected) {
            return;
        }

        playerInTrigger = false;
    }

}