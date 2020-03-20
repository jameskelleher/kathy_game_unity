using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private AudioSource audioSource;

    public AudioClip[] clips;

    [HideInInspector]
    public int clip_ix = 0;

    void Awake () {
        if (Instance == null) {
            DontDestroyOnLoad (gameObject);
            Instance = this;
            audioSource = GetComponent<AudioSource> ();
            SceneManager.sceneLoaded += OnSceneLoaded;

        } else if (Instance != this) {
            Destroy (gameObject);
        }

        // initialize for random spawn point on main map
        spawnAtX = Random.Range (-3, 3);
        spawnAtY = Random.Range (-3, 3);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        print("scene loaded");
        if (scene.name == "Club") {
            print("playing music");
            audioSource.clip = clips[clip_ix];
            audioSource.Play();
        } else {
            audioSource.Stop();
        }
    }
}