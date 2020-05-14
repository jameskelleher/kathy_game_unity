using System.Collections;
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

    private bool canMove;

    public int coinCount;

    private int maxCoinCount = 3;

    #endregion

    #region Audio

    public SongDataScriptableObject songData;

    private AudioSource audioSource;

    #endregion

    #region Scene Transition Data

    [HideInInspector]
    private string spawnPointName;

    float spawnAtX;

    float spawnAtY;

    [HideInInspector]
    public Vector2 lastMove;

    #endregion

    void Awake () {

        canMove = true;

        if (Instance == null) {
            DontDestroyOnLoad (gameObject);
            Instance = this;
            audioSource = GetComponent<AudioSource> ();
            SceneManager.sceneLoaded += OnSceneLoaded;

        } else if (Instance != this) {
            Destroy (gameObject);
        }

        // initialize for random spawn point on main map
        spawnAtX = -3;
        spawnAtY = Random.Range (-3, 0);

        coinCount = 0;
    }

    void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        sceneName = SceneManager.GetActiveScene ().name;

        if (scene.name == "Club" && songData != null) {
            audioSource.clip = songData.clip;
            audioSource.Play ();
        } else {
            audioSource.Stop ();
        }

        if (scene.name != "Launcher") {
            if (!string.IsNullOrEmpty (spawnPointName)) {
                GameObject spawnPoint = GameObject.Find (spawnPointName);
                spawnAtX = spawnPoint.transform.position.x;
                spawnAtY = spawnPoint.transform.position.y;
            }
            // photon remembers the initial spawnpoint, so we have to spawn everyone offscreen,
            // then update the position later
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

    public bool GetCanMove () {
        return canMove;
    }
    public void SetCanMove (bool canMove) {
        this.canMove = canMove;
    }

    public Vector2 GetSpawnPoint () {
        return new Vector2 (spawnAtX, spawnAtY);
    }

    public void SetSpawnPointName (string spawnPointName) {
        this.spawnPointName = spawnPointName;
    }

    public void AddCoin () {
        if (coinCount < maxCoinCount) coinCount++;
    }

    public void SubtractCoin() {
        coinCount--;
    }

    public bool CheckCoinCount () {
        return coinCount > 0;
    }

}