using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Song Data", menuName = "ScriptableObjects/SongDataScriptableObject", order = 1)]
public class SongDataScriptableObject : ScriptableObject {
    public string juiceFlavorName;
    public AudioClip clip;
    public Gradient clubGradient;

}
