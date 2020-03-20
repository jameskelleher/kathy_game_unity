using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour {
    
    public int clip_ix;

    public void SetAudio() {
        
        print("setting audio");
        GameManager.Instance.clip_ix = clip_ix;
        // TODO: fix this mess
        this.gameObject.transform.parent.parent.gameObject.SetActive(false);
    }

}