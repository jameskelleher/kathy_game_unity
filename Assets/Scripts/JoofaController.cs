using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class JoofaController : MonoBehaviour {
    public GameObject choosePanel;

    bool playerInTrigger;

    Animator animator;

    void Start () {
        animator = GetComponent<Animator> ();
        SpinAnimation ();
    }

    void Update () {
        this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.y);

        bool inputCheck = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
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

    void SpinAnimation () {
        StartCoroutine ("SpinAnimationCoroutine");
    }

    IEnumerator SpinAnimationCoroutine () {
        yield return new WaitForSeconds (1f);
        animator.Play ("joofa_spin");
    }
}