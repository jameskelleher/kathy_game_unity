using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZToY : MonoBehaviour {
    public float offset = 0;
    public bool debug = false;
    // Start is called before the first frame update
    void Start () {
        UpdatePosition();
    }
    void LateUpdate () {
        if (debug) {
            UpdatePosition();
        }
    }

    private void UpdatePosition () {
        print("updating position");
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        this.transform.position = new Vector3 (x, y, y - offset);
    }

}