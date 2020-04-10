using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientController : MonoBehaviour {
    public Gradient gradient2;
    Gradient gradient;

    Camera my_camera;

    GradientColorKey[] colorKeys;
    GradientAlphaKey[] alphaKeys;

    public float gradTime = 1;

    public Color[] colors;

    float evalAt;

    int incr;

    public bool debug;
    public float debugEval;

    void Start () {
        evalAt = 0f;
        incr = 1;

        my_camera = this.GetComponent<Camera> ();

        int numColors = colors.Length;

        gradient = new Gradient ();
        colorKeys = new GradientColorKey[numColors];
        alphaKeys = new GradientAlphaKey[numColors];
        

        for (int i = 0; i < numColors; i++) {
            float time = ((float) i) / (numColors - 1);
            print("setting color " + i.ToString() + " at time " + time.ToString());

            colorKeys[i].color = colors[i];
            colorKeys[i].time = time;
            alphaKeys[i].alpha = 0.0f;
            alphaKeys[i].time = time;
        }

        gradient.SetKeys (colorKeys, alphaKeys);
        // my_camera.backgroundColor = Color.red;
    }

    void Update () {
        Color backgroundColor;

        if (debug) {
            backgroundColor = gradient.Evaluate (debugEval);
        } else {
            if (gradTime <=0 ) {
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