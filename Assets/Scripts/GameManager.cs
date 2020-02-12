using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;
    // Start is called before the first frame update
    void Start () {
        foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
            if (gameObj.name == player.name) {
                print("found one");
            }
            // StartCoroutine (activateProcedure (gameObj));

        }
        // float randX = Random.Range (-5, 5);
        // float randY = Random.Range (-5, 5);
        // Vector3 startPos = new Vector3(randX, randY, 0f);
        Vector3 startPos = new Vector3(-100, -100, 0);
        GameObject newPlayer = PhotonNetwork.Instantiate (player.name, startPos, Quaternion.identity);
    }

    IEnumerator activateProcedure (GameObject newPlayer) {
        newPlayer.SetActive (false);
        yield return new WaitForSeconds (1f);
        newPlayer.SetActive (true);
    }

}