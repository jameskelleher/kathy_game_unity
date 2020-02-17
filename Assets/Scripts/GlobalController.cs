using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour {

    public int outfit_ix;

    public static GlobalController Instance;

    void Awake () {
        if (Instance == null) {
            DontDestroyOnLoad (gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy (gameObject);
        }
    }
}