using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviourPun, IPunObservable {

    [HideInInspector]
    public string currentScene;

    public GameObject emote;

    #region Movement

    private Vector3 pos;

    public float moveSpeed = 3f;
    public float interpolationAmount = 4f;

    [HideInInspector]
    public int updatedFrames = 0;
    private int startUpdatingAt = 60;
    #endregion

    #region Animation
    private Animator animator;

    private float h;
    private float v;
    private Vector2 lastMove = new Vector2 (0f, 0f);
    private bool playerMoving = false;

    #endregion

    void Start () {

        animator = GetComponent<Animator> ();

        if (photonView.IsMine || !PhotonNetwork.IsConnected) {

            float x = GameManager.Instance.spawnAtX;
            float y = GameManager.Instance.spawnAtY;
            Vector3 startPos = new Vector3 (x, y, y);

            this.gameObject.transform.position = startPos;

            this.lastMove = GameManager.Instance.lastMove;

        }

        DontDestroyOnLoad (this.gameObject);

        // if player object created in a different scene, deactivate it
        if (PhotonNetwork.IsConnected && !photonView.IsMine) {
            string localPlayerscene = SceneManager.GetActiveScene ().name;
            currentScene = (string) photonView.InstantiationData[0];

            if (currentScene != localPlayerscene) {
                this.gameObject.SetActive (false);
            }
        }
    }

    void Update () {
        if (photonView.IsMine || !PhotonNetwork.IsConnected) {
            checkInput ();

            // if (Input.GetKeyDown ("q")) {
            //     string thisScene = SceneManager.GetActiveScene ().name;
            //     if (thisScene == "Main") {
            //         PhotonNetwork.LoadLevel ("Joose");
            //     } else {
            //         PhotonNetwork.LoadLevel ("Main");
            //     }
            //     PhotonNetwork.Destroy (gameObject);
            // }
        } else if (!photonView.IsMine) {
            UpdateFromPhoton ();
        }

        // update z-depth
        this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.y);
    }

    private void checkInput () {
        if (GameManager.Instance.GetCanMove ()) {
            h = Input.GetAxisRaw ("Horizontal");
            v = Input.GetAxisRaw ("Vertical");

            if (Input.GetKeyDown ("space") && emote) {
                PhotonNetwork.Instantiate(emote.name, this.gameObject.transform.position, Quaternion.identity);
                // Instantiate (emote, this.gameObject.transform.position, Quaternion.identity);
            }

        } else {
            h = 0;
            v = 0;

        }

        playerMoving = false;

        if (h != 0) {
            float moveX = h * moveSpeed * Time.deltaTime;
            transform.Translate (new Vector3 (moveX, 0f, 0f));
            playerMoving = true;
            lastMove = new Vector2 (h, 0f);
        }
        if (v != 0) {
            float moveY = v * moveSpeed * Time.deltaTime;
            transform.Translate (new Vector3 (0f, moveY, 0f));
            playerMoving = true;
            lastMove = new Vector2 (0f, v);

        }

        UpdateAnimator ();

    }

    void UpdateFromPhoton () {
        if (updatedFrames < startUpdatingAt) {
            updatedFrames++;
        } else if (updatedFrames == startUpdatingAt) {
            gameObject.transform.position = pos;
            updatedFrames++;
        } else {
            gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, pos, interpolationAmount * Time.deltaTime);
            UpdateAnimator ();

        }
    }

    void UpdateAnimator () {
        animator.SetFloat ("MoveX", h);
        animator.SetFloat ("MoveY", v);
        animator.SetFloat ("LastMoveX", lastMove.x);
        animator.SetFloat ("LastMoveY", lastMove.y);
        animator.SetBool ("PlayerMoving", playerMoving);
    }

    public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo messageInfo) {
        if (stream.IsWriting) {
            stream.SendNext (transform.position);
            stream.SendNext (h);
            stream.SendNext (v);
            stream.SendNext (lastMove);
            stream.SendNext (playerMoving);
        } else {
            pos = (Vector3) stream.ReceiveNext ();
            h = (float) stream.ReceiveNext ();
            v = (float) stream.ReceiveNext ();
            lastMove = (Vector2) stream.ReceiveNext ();
            playerMoving = (bool) stream.ReceiveNext ();
        }
    }

}