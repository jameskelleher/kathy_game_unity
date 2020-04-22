using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour {

    public SongDataScriptableObject songData;
    
    GameObject chooseMenu;

    void Start () {


        chooseMenu = GameObject.Find ("Choose Menu");
        if (!chooseMenu) {
            Debug.LogWarning ("Warning: could not find Choose Menu");
        }
    }

    public void SetAudio () {

        print ("setting audio");
        GameManager.Instance.songData = songData;
        GameManager.Instance.SetCanMove (true);
        GameManager.Instance.AddCoin();

        // TODO: fix this mess
        chooseMenu.SetActive (false);

    }

}