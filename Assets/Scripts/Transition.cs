using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Transition : MonoBehaviour {
    public string loadLevel;
    public string spawnPointName;
    public float setLastMoveX;
    public float setLastMoveY;

    void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.GetComponent<PlayerController>()) {
            PhotonView photonView = other.gameObject.GetComponent<PhotonView> ();
            if (photonView.IsMine) {
                GameManager.Instance.SetSpawnPointName(spawnPointName);
                GameManager.Instance.lastMove = new Vector2(setLastMoveX, setLastMoveY);

                PhotonNetwork.Destroy(other.gameObject);
                PhotonNetwork.LoadLevel(loadLevel);
            }
        }
    }
}