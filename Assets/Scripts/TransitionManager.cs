using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour {

    public GameObject transitionImageObject;

    public GameObject audioSourceObject;

    public float secondsToFade = 0.5f;

    Image transitionImage;

    bool doFadein = true;
    bool doFadeout = false;
    bool didDestroy = false;

    string loadLevel;

    Collider2D toDestroy;

    void Awake () {
        transitionImage = transitionImageObject.GetComponent<Image> ();

        if (audioSourceObject == null) {
            audioSourceObject = GameManager.Instance.gameObject;
        }

        Color color = transitionImage.color;
        color.a = 1.0f;
        transitionImage.color = color;

        // start the first audio source
        audioSourceObject.GetComponents<AudioSource>()[0].volume = 1f;
    }

    void Update () {
        if (doFadein) {
            Color color = transitionImage.color;
            float incr = Time.deltaTime / secondsToFade;
            color.a -= incr;
            transitionImage.color = color;

            if (color.a <= 0f) {
                GameManager.Instance.SetCanMove (true);
                doFadein = false;
            }
        } else if (doFadeout) {
            Color color = transitionImage.color;
            float incr = Time.deltaTime / secondsToFade;

            color.a += incr;
            transitionImage.color = color;

            foreach (AudioSource as_ in audioSourceObject.GetComponents<AudioSource> ()) {
                as_.volume -= incr;
            }

            if (color.a >= 1f) DoTransition ();
        }

    }

    public void StartFadeout (string loadLevel, Collider2D toDestroy) {
        doFadeout = true;
        this.loadLevel = loadLevel;
        this.toDestroy = toDestroy;

        toDestroy.gameObject.SetActive (false);

        GameManager.Instance.SetCanMove (false);

    }

    void DoTransition () {
        if (didDestroy) return;
        PhotonNetwork.Destroy (this.toDestroy.gameObject);
        PhotonNetwork.LoadLevel (loadLevel);
        didDestroy = true;
    }

}