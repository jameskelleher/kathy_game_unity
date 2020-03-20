// using System.Collections;
// using System.Collections.Generic;
// using Photon.Pun;
// using Photon.Realtime;
// using UnityEngine;

// public class DEPR_PhotonConnect : MonoBehaviourPunCallbacks {

//     [SerializeField]
//     private byte maxPlayersPerRoom = 4;
//     public GameObject sectionView1, sectionView2, sectionView3;

//     public void connectToPhoton (int outfit_ix) {

//         GlobalController.Instance.outfit_ix = outfit_ix;
        
//         PhotonNetwork.ConnectUsingSettings ();

//         Debug.Log ("connecting to photon...");
//     }

//     public override void OnConnectedToMaster () {
//         Debug.Log ("we are connected to master");
//         sectionView1.SetActive (false);
//         sectionView2.SetActive (true);
//         PhotonNetwork.JoinRandomRoom ();
//     }

//     public override void OnJoinRandomFailed (short returnCode, string message) {
//         Debug.Log ("couldn't find room, making one");
//         PhotonNetwork.CreateRoom (null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
//     }

//     public override void OnJoinedRoom () {
//         Debug.Log ("On Joined Room");

//         PhotonNetwork.LoadLevel("Main");
//     }

//     private void OnDisconnectedFromPhoton () {
//         sectionView1.SetActive (false);
//         sectionView2.SetActive (false);
//         sectionView3.SetActive (true);
//         Debug.Log ("Disconnected from photon services");
//     }

// }