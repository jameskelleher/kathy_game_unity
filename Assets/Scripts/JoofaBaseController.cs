using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class JoofaBaseController : MonoBehaviour {
    public GameObject choosePanel;
    
    bool playerInTrigger;
    

    void Update () {
        bool inputCheck = Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift);
        if (inputCheck && playerInTrigger) {
            GameManager.Instance.SetCanMove (false);
            choosePanel.SetActive (true);
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