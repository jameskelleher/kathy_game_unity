using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour {

    public int clip_ix;

    GameObject chooseMenu;

    void Start () {
        chooseMenu = GameObject.Find ("Choose Menu");
        if (!chooseMenu) {
            Debug.LogWarning ("Warning: could not find Choose Menu");
        }
    }

    public void SetAudio () {

        print ("setting audio");
        GameManager.Instance.clip_ix = clip_ix;
        GameManager.Instance.SetCanMove (true);
        GameManager.Instance.AddCoin();

        // TODO: fix this mess
        chooseMenu.SetActive (false);

    }

}