using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour {

    public int outfit_ix;

    [HideInInspector]
    public static GlobalController Instance;

    [HideInInspector]
    public float spawnAtX;

    [HideInInspector]
    public float spawnAtY;

    [HideInInspector]
    public Vector2 lastMove;

    [HideInInspector]
    private AudioSource audioSource;

    void Awake () {
        if (Instance == null) {
            DontDestroyOnLoad (gameObject);
            Instance = this;
            audioSource = GetComponent<AudioSource> ();

        } else if (Instance != this) {
            Destroy (gameObject);
        }

        // initialize for random spawn point on main map
        spawnAtX = Random.Range (-3, 3);
        spawnAtY = Random.Range (-3, 3);
    }

    public void PlaySong(AudioClip clip) {
        print("playing");
        audioSource.clip = clip;
        audioSource.Play();
    }
}