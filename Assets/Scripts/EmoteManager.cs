using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteManager : MonoBehaviour
{
    public GameObject emote;
    void Update() {
        if (Input.GetKeyDown("space")) {
            Instantiate(emote, Vector3.zero, Quaternion.identity);
        }
    }
}
