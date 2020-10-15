using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMenuController : MonoBehaviour
{
    public GameObject onEnableSoundObject;

    public GameObject onDisableSoundObject;

    void OnEnable() {
        print("enable");
        onEnableSoundObject.GetComponent<AudioSource>().Play();
    }

    void OnDisable() {
        print("disable");
        onDisableSoundObject.GetComponent<AudioSource>().Play();
    }
    
}
