using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public GameObject player;

    private string sceneName;

    void Start () {
        Vector3 startPos = new Vector3 (-100, -100, 0);
        sceneName = SceneManager.GetActiveScene ().name;
        object[] data = new object[] { GlobalController.Instance.outfit_ix, sceneName };
        GameObject newPlayer = PhotonNetwork.Instantiate (player.name, startPos, Quaternion.identity, 0, data);
        PlayerController[] playerControllers = Resources.FindObjectsOfTypeAll<PlayerController> ();
        foreach (PlayerController playerController in playerControllers) {
            PhotonView pv = playerController.photonView;
            print(pv);
            if (pv.IsMine || pv.InstantiationId == 0 || pv.InstantiationData == null) {
                continue;
            }
            string playerSceneName = (string) pv.InstantiationData[1];
            if (sceneName == playerSceneName) {
                playerController.gameObject.transform.position = new Vector3(100f, 100f, 0f);
                playerController.updatedFrames = 0;
                playerController.gameObject.SetActive(true);
            } else {
                playerController.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator activateProcedure (GameObject newPlayer) {
        newPlayer.SetActive (false);
        yield return new WaitForSeconds (1f);
        newPlayer.SetActive (true);
    }

}