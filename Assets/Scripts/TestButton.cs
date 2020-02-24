using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour {
    public AudioClip clip;

    public void PlayAudio() {
        GlobalController.Instance.PlaySong(clip);
        this.gameObject.transform.parent.parent.gameObject.SetActive(false);
    }

}