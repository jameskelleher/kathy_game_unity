using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Transition : MonoBehaviour {
    public float spawnAtX;
    public float spawnAtY;
    public float lastMoveX;
    public float lastMoveY;

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "Player") {
            PhotonView photonView = other.gameObject.GetComponent<PhotonView> ();
            if (photonView.IsMine) {
                GlobalController.Instance.spawnAtX = spawnAtX;
                GlobalController.Instance.spawnAtY = spawnAtY;
                GlobalController.Instance.lastMove = new Vector2(lastMoveX, lastMoveY);
                
                string sceneName = SceneManager.GetActiveScene ().name;
                if (sceneName == "Main") {
                    PhotonNetwork.Destroy (other.gameObject);
                    PhotonNetwork.LoadLevel ("Joose");
                } else {
                    PhotonNetwork.Destroy (other.gameObject);
                    PhotonNetwork.LoadLevel ("Main");
                }
            }
        }
    }
}