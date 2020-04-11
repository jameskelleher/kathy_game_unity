using Photon.Pun;
using UnityEngine;

public class FountainController : MonoBehaviourPun {
    // flag that allows player to interact with the fountain
    bool playerInTrigger;

    Animator animator;

    void Start () {
        // initialize variables
        playerInTrigger = false;

        animator = this.GetComponent<Animator> ();
    }

    void Update () {

        if (playerInTrigger && Input.GetKeyDown ("right shift")) {
            // play animation if triggered by player
            photonView.RPC ("PlayAnim", RpcTarget.All);
        }
    }

    // used in Animation to end the clip
    void TurnOffAnimation () {
        animator.SetBool ("PlayAnimation", false);
    }

    // allow the event to happen if player's character enters trigger
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

    // prevent the event from happening if player's character leaves trigger
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

    [PunRPC]
    void PlayAnim () {
        animator.SetBool ("PlayAnimation", true);
    }

}