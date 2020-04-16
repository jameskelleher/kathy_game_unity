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
        CheckAction ();
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

    void CheckAction () {
        bool inputCheck = Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift);
        if (inputCheck && playerInTrigger) {
            bool playingAnimation = animator.GetBool ("PlayAnimation");
            bool playerCanThrowCoin = GameManager.Instance.CheckCoinCount ();
            if (playerCanThrowCoin && !playingAnimation) {
                // play animation, broadcast to all players
                photonView.RPC ("PlayAnim", RpcTarget.All);
                GameManager.Instance.SubtractCoin ();
            }
        }
    }

}