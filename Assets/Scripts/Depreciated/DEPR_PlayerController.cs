﻿// using System.Collections;
// using System.Collections.Generic;
// using Photon.Pun;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class DEPR_PlayerController : MonoBehaviourPun, IPunObservable {

//     public GameObject[] outfits;

//     public GameObject body;

//     private Vector3 pos;

//     public float moveSpeed = 1f;
//     public float interpolationAmount = 4f;

//     [HideInInspector]
//     public int updatedFrames = 0;
//     private int startUpdatingAt = 60;

//     private Animator animator;

//     #region Animation Parameters

//     private float h;
//     private float v;
//     private Vector2 lastMove = new Vector2 (0f, 0f);
//     private bool playerMoving = false;

//     #endregion

//     void Start () {
//         int outfit_ix;

//         animator = GetComponent<Animator> ();

//         if (photonView.IsMine || !PhotonNetwork.IsConnected) {
//             float x = GlobalController.Instance.spawnAtX;
//             float y = GlobalController.Instance.spawnAtY;
//             Vector3 startPos = new Vector3(x, y, 0f);

//             this.gameObject.transform.position = startPos;

//             this.lastMove = GlobalController.Instance.lastMove;

//             outfit_ix = GlobalController.Instance.outfit_ix;
//         } else {
//             object[] data = photonView.InstantiationData;
//             outfit_ix = (int) data[0];
//         }

//         GameObject body_inst = Instantiate(body, this.gameObject.transform.position, Quaternion.identity);
//         body_inst.transform.parent = this.transform;

//         GameObject outfit = outfits[outfit_ix];
//         Vector3 outfit_position = new Vector3(transform.position.x, transform.position.y, -0.00001f);

//         GameObject outfit_inst = Instantiate(outfit, outfit_position, Quaternion.identity);
//         outfit_inst.transform.parent = this.transform;

//         DontDestroyOnLoad(this.gameObject);

//         if (!photonView.IsMine) {
//             string thisScene = SceneManager.GetActiveScene().name;
//             string otherPlayerScene = (string) photonView.InstantiationData[1];

//             if (thisScene != otherPlayerScene) {
//                 this.gameObject.SetActive(false);
//             }
//         }
//     }

//     void Update () {
//         if (photonView.IsMine || !PhotonNetwork.IsConnected) {
//             checkInput ();

//             if (Input.GetKeyDown("q")) {
//                 string thisScene = SceneManager.GetActiveScene().name;
//                 if (thisScene == "Main") {
//                     PhotonNetwork.LoadLevel("Joose");
//                 } else {
//                     PhotonNetwork.LoadLevel("Main");
//                 }
//                 PhotonNetwork.Destroy(gameObject);
//             }
//         } else if (!photonView.IsMine) {
//             if (updatedFrames < startUpdatingAt) {
//                 updatedFrames++;
//             } else if (updatedFrames == startUpdatingAt) {
//                 gameObject.transform.position = pos;
//                 updatedFrames++;
//             } else {
//                 gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, pos, interpolationAmount * Time.deltaTime);
//                 animator.SetFloat ("MoveX", h);
//                 animator.SetFloat ("MoveY", v);
//                 animator.SetFloat ("LastMoveX", lastMove.x);
//                 animator.SetFloat ("LastMoveY", lastMove.y);
//                 animator.SetBool ("PlayerMoving", playerMoving);

//                 Animator[] child_animators = GetComponentsInChildren<Animator> ();
//                 foreach (Animator anim in child_animators) {
//                     anim.SetFloat ("MoveX", h);
//                     anim.SetFloat ("MoveY", v);
//                     anim.SetFloat ("LastMoveX", lastMove.x);
//                     anim.SetFloat ("LastMoveY", lastMove.y);
//                     anim.SetBool ("PlayerMoving", playerMoving);
//                 }

//             }
//         }

//         this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.y);
//     }

//     private void checkInput () {
//         h = Input.GetAxisRaw ("Horizontal");
//         v = Input.GetAxisRaw ("Vertical");

//         playerMoving = false;

//         if (h != 0) {
//             float moveX = h * moveSpeed * Time.deltaTime;
//             transform.Translate (new Vector3 (moveX, 0f, 0f));
//             playerMoving = true;
//             lastMove = new Vector2 (h, 0f);
//         }
//         if (v != 0) {
//             float moveY = v * moveSpeed * Time.deltaTime;
//             transform.Translate (new Vector3 (0f, moveY, 0f));
//             playerMoving = true;
//             lastMove = new Vector2 (0f, v);
//         }

//         animator.SetFloat ("MoveX", h);
//         animator.SetFloat ("MoveY", v);
//         animator.SetFloat ("LastMoveX", lastMove.x);
//         animator.SetFloat ("LastMoveY", lastMove.y);
//         animator.SetBool ("PlayerMoving", playerMoving);

//         Animator[] child_animators = GetComponentsInChildren<Animator> ();
//         foreach (Animator anim in child_animators) {
//             anim.SetFloat ("MoveX", h);
//             anim.SetFloat ("MoveY", v);
//             anim.SetFloat ("LastMoveX", lastMove.x);
//             anim.SetFloat ("LastMoveY", lastMove.y);
//             anim.SetBool ("PlayerMoving", playerMoving);
//         }

//     }

//     public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo messageInfo) {
//         if (stream.IsWriting) {
//             stream.SendNext (transform.position);
//             stream.SendNext (h);
//             stream.SendNext (v);
//             stream.SendNext (lastMove);
//             stream.SendNext (playerMoving);
//         } else {
//             pos = (Vector3) stream.ReceiveNext ();
//             h = (float) stream.ReceiveNext ();
//             v = (float) stream.ReceiveNext ();
//             lastMove = (Vector2) stream.ReceiveNext ();
//             playerMoving = (bool) stream.ReceiveNext ();
//         }
//     }

//     // private void 
// }