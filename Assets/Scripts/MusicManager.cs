using UnityEngine;

public class MusicManager : MonoBehaviour {
    public AudioSource[] musicSources;

    public float musicBPM, timeSignature, barsLength;

    private float loopPointMintues, loopPointSeconds;
    private double time;

    private int nextSource;

    void Start() {
        loopPointMintues = (barsLength * timeSignature) / musicBPM;

        print("bars length = " + barsLength);
        print("time signature = " + timeSignature);
        print("music BPM = " + musicBPM);

        print("loop point minutes = " + loopPointMintues);

        loopPointSeconds = loopPointMintues * 60;


        time = AudioSettings.dspTime;
        // print(time);
        musicSources[0].Play();
        nextSource = 1;

        print("loop point seconds = " + loopPointSeconds);
    }

    void Update() {
        if (!musicSources[nextSource].isPlaying) {
            time = time + loopPointSeconds;
            // time = loopPointSeconds;
            // print("source:");
            // print(nextSource);
            // print("scheduled in:");
            // print(time);
            musicSources[nextSource].PlayScheduled(time);
            nextSource = 1 - nextSource;  // switch to other AudioSource
        }
    }
}
