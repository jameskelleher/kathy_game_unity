using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour {
    
    public AudioClip clip;

    public void SetAudio() {
        
        print("setting audio");
        GlobalController.Instance.clip = clip;
        this.gameObject.transform.parent.parent.gameObject.SetActive(false);
    }

}