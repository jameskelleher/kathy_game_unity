using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmoteHandler : MonoBehaviourPun {
    // public float timeToDestroy = 10;

    public float moveSpeed = 0.1f;

    public float secondsToFade = 0.1f;

    string sceneName;

    string currentScene;
    SpriteRenderer sprite;

    void Start () {
        sprite = this.GetComponent<SpriteRenderer> ();

        currentScene = SceneManager.GetActiveScene ().name;
        sceneName = (string) photonView.InstantiationData[0];
        print("sceneName " + sceneName);
        print("currentScene " + currentScene);
        if (sceneName != currentScene) {
            this.gameObject.SetActive(false);
        }
    }

    void Update () {
        Color color;
        Vector3 pos = this.gameObject.transform.position;
        Vector3 newPos = new Vector3 (pos.x, pos.y + moveSpeed * Time.deltaTime, pos.z);
        this.gameObject.transform.position = newPos;

        color = sprite.color;
        color.a -= Time.deltaTime / secondsToFade;

        if (color.a <= 0) {
            PhotonNetwork.Destroy (this.gameObject);
        }

        sprite.color = color;
    }
}

