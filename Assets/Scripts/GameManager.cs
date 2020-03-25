﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    [HideInInspector]
    public static GameManager Instance;

    private string sceneName;

    #region Player
    [HideInInspector]
    public int char_ix;

    public GameObject[] characters;

    private GameObject player;

    #endregion

    #region Audio
    public AudioClip[] clips;

    // [HideInInspector]
    public int clip_ix;

    private AudioSource audioSource;

    #endregion

    #region Scene Transition Data

    [HideInInspector]
    public float spawnAtX;

    [HideInInspector]
    public float spawnAtY;

    [HideInInspector]
    public Vector2 lastMove;

    #endregion

    void Awake () {
        clip_ix = -1;
        if (Instance == null) {
            DontDestroyOnLoad (gameObject);
            Instance = this;
            audioSource = GetComponent<AudioSource> ();
            SceneManager.sceneLoaded += OnSceneLoaded;

        } else if (Instance != this) {
            Destroy (gameObject);
        }

        // initialize for random spawn point on main map
        spawnAtX = Random.Range (-3, 0);
        spawnAtY = Random.Range (-3, 0);
    }

    void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        sceneName = SceneManager.GetActiveScene ().name;

        if (scene.name == "Club" && clip_ix >= 0) {
            audioSource.clip = clips[clip_ix];
            audioSource.Play ();
        } else {
            audioSource.Stop ();
        }

        if (scene.name != "Launcher") {
            Vector3 startPos = new Vector3 (-100, -100, 0);
            object[] data = new object[] { sceneName };
            player = PhotonNetwork.Instantiate (characters[char_ix].name, startPos, Quaternion.identity, 0, data);

            // you only want to see other players if they are in the same scene
            PlayerController[] playerControllers = Resources.FindObjectsOfTypeAll<PlayerController> ();
            foreach (PlayerController playerController in playerControllers) {
                PhotonView pv = playerController.photonView;
                if (pv.IsMine || pv.InstantiationId == 0 || pv.InstantiationData == null) {
                    continue;
                }
                string playerSceneName = playerController.currentScene;
                if (sceneName == playerSceneName) {
                    if (playerController.gameObject.activeInHierarchy == false) {
                        playerController.gameObject.transform.position = new Vector3 (100f, 100f, 0f);
                        playerController.updatedFrames = 0;
                        playerController.gameObject.SetActive (true);
                    }
                } else {
                    playerController.gameObject.SetActive (false);
                }
            }
        }
    }

}