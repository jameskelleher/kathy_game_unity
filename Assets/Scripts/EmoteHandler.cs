using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EmoteHandler : MonoBehaviour {
    // public float timeToDestroy = 10;

    public float moveSpeed = 0.1f;

    public float secondsToFade = 0.1f;

    // void Start() {
    //     Destroy(this.gameObject, timeToDestroy);
    // }

    void Update() {
        Vector3 pos = this.gameObject.transform.position;
        Vector3 newPos = new Vector3(pos.x, pos.y + moveSpeed * Time.deltaTime, pos.z);
        this.gameObject.transform.position = newPos;

        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        Color color = sprite.color;
        color.a -= Time.deltaTime / secondsToFade;

        if (color.a <= 0) {
            PhotonNetwork.Destroy(this.gameObject);
        }

        sprite.color = color;

    }

}