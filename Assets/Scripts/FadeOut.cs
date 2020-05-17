using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

    public GameObject fadeoutImageObject;

    public float secondsToFade = 0.5f;

    Image fadeoutImage;

    bool doFadeout = false;
    bool didDestroy = false;

    string loadLevel;

    Collider2D toDestroy;

    void Start () {
        fadeoutImage = fadeoutImageObject.GetComponent<Image> ();
    }

    void Update () {
        if (!doFadeout) return;
        Color color = fadeoutImage.color;
        color.a += Time.deltaTime / secondsToFade;
        fadeoutImage.color = color;

        if (color.a >= 1f) DoTransition ();
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