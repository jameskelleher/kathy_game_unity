using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour {

    public int outfit_ix;

    public static GlobalController Instance;

    [HideInInspector]
    public float spawnAtX;

    [HideInInspector]
    public float spawnAtY;

    [HideInInspector]
    public Vector2 lastMove;

    void Awake () {
        if (Instance == null) {
            DontDestroyOnLoad (gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy (gameObject);
        }

        // initialize for random spawn point on main map
        spawnAtX = Random.Range (-3, 3);
        spawnAtY = Random.Range (-3, 3);
    }
}