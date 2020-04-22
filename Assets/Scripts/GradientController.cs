using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientController : MonoBehaviour {
    public Gradient gradient;

    Camera my_camera;

    public float gradTime = 1;

    float evalAt;

    int incr;

    public bool debug;
    public float debugEval;

    void Start () {
        evalAt = 0f;
        incr = 1;

        my_camera = this.GetComponent<Camera> ();

    }

    void Update () {

        Color backgroundColor;

        if (debug) {
            backgroundColor = gradient.Evaluate (debugEval);
        } else {
            if (gradTime <= 0) {
                gradTime = 1;
            }
            evalAt += (incr * Time.deltaTime) / gradTime;
            backgroundColor = gradient.Evaluate (evalAt);
            if (evalAt >= 1) {
                incr = -1;
            } else if (evalAt <= 0) {
                incr = 1;
            }
        }

        my_camera.backgroundColor = backgroundColor;
    }
}