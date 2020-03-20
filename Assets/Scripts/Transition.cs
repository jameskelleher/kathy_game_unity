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
    public string loadLevel;

    void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.GetComponent<PlayerController>()) {
            PhotonView photonView = other.gameObject.GetComponent<PhotonView> ();
            if (photonView.IsMine) {
                GameManager.Instance.spawnAtX = spawnAtX;
                GameManager.Instance.spawnAtY = spawnAtY;
                GameManager.Instance.lastMove = new Vector2(lastMoveX, lastMoveY);

                PhotonNetwork.Destroy(other.gameObject);
                PhotonNetwork.LoadLevel(loadLevel);
            }
        }
    }
}