using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun, IPunObservable {

    private Vector3 pos;

    public float moveSpeed = 1f;
    public float interpolationAmount = 4f;

    // private bool firstUpdate = true;
    private int updatedFrames = 0;
    private int startUpdatingAt = 60;

    private Animator animator;
    private bool playerMoving;
    private Vector2 lastMove;

    void Update () {
        if (photonView.IsMine || !PhotonNetwork.IsConnected) {
            checkInput ();
        } else if (!photonView.IsMine) {
            if (updatedFrames < startUpdatingAt) {
                updatedFrames++;
            } else if (updatedFrames == startUpdatingAt) {
                gameObject.transform.position = pos;
                updatedFrames++;
            } else {
                print ("doing lerp");
                gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, pos, interpolationAmount * Time.deltaTime);
            }
        }
    }

    void Start () {

        animator = GetComponent<Animator>();

        if (photonView.IsMine) {
            float randX = Random.Range (-5, 5);
            float randY = Random.Range (-5, 5);
            Vector3 startPos = new Vector3 (randX, randY, 0);

            gameObject.transform.position = startPos;
        }
    }

    private void checkInput () {
        float h = Input.GetAxisRaw ("Horizontal");
        float v = Input.GetAxisRaw ("Vertical");

        playerMoving = false;

        if (h != 0) {
            float moveX = h * moveSpeed * Time.deltaTime;
            transform.Translate (new Vector3 (moveX, 0f, 0f));
            playerMoving = true;
            lastMove = new Vector2(h, 0f);
        }
        if (v != 0) {
            float moveY = v * moveSpeed * Time.deltaTime;
            transform.Translate (new Vector3 (0f, moveY, 0f));
            playerMoving = true;
            lastMove = new Vector2(0f, v);
        }

        animator.SetFloat("MoveX", h);
        animator.SetFloat("MoveY", v);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
        animator.SetBool("PlayerMoving", playerMoving);
    }

    public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo messageInfo) {
        if (stream.IsWriting) {
            stream.SendNext (transform.position);
        } else {
            pos = (Vector3) stream.ReceiveNext ();
        }
    }

    // private void 
}