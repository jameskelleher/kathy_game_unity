using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Transition : MonoBehaviour {
    public string loadLevel;
    public string spawnPointName;
    public float setLastMoveX;
    public float setLastMoveY;

    public GameObject fadeout;

    public float secondsToFade = 0.5f;

    Image fadeoutImage;

    bool doFadeout;

    Collider2D toDestroy;

    void Start() {
        fadeoutImage = fadeout.GetComponent<Image>();
        doFadeout = false;
    }

    void Update() {
        
        if (doFadeout) {
            if (fadeoutImage is null) doTransition();

            Color color = fadeoutImage.color;
            color.a += Time.deltaTime / secondsToFade;
            fadeoutImage.color = color;

            if (color.a >= 1f) doTransition();
        }
    }

    void doTransition() {
        PhotonNetwork.Destroy(toDestroy.gameObject);
        PhotonNetwork.LoadLevel(loadLevel);
    }


    void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.GetComponent<PlayerController>()) {
            PhotonView photonView = other.gameObject.GetComponent<PhotonView> ();
            if (photonView.IsMine) {
                GameManager.Instance.SetSpawnPointName(spawnPointName);
                GameManager.Instance.lastMove = new Vector2(setLastMoveX, setLastMoveY);

                doFadeout = true;
                toDestroy = other;
                toDestroy.gameObject.SetActive(false);

                GameManager.Instance.SetCanMove(false);
                
            }
        }
    }
}