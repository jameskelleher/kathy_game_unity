using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour {
    
    public int clip_ix;

    public void SetAudio() {
        
        print("setting audio");
        GlobalController.Instance.clip_ix = clip_ix;
        this.gameObject.transform.parent.parent.gameObject.SetActive(false);
    }

}