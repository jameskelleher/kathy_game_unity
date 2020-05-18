using UnityEngine;

public class ClubSceneController : MonoBehaviour {
    public GameObject clubEffects;

    public GameObject[] animated;

    public Camera sceneCamera;

    void Start () {
        if (GameManager.Instance.songData == null) {
            clubEffects.SetActive (false);

            foreach (GameObject obj in animated) {
                Animator anim = obj.GetComponent<Animator> ();
                anim.enabled = false;
            }
        } else {
            GradientController gradientController = sceneCamera.GetComponent<GradientController> ();
            gradientController.gradient = GameManager.Instance.songData.clubGradient;

            foreach (GameObject obj in animated) {
                if (obj.tag == "Speaker") {
                    Animator anim = obj.GetComponent<Animator> ();
                    float speedMultiplier = GameManager.Instance.songData.BPM / 120f;
                    print(speedMultiplier);
                    anim.SetFloat("speedMultiplier", speedMultiplier);
                }
            }
        }
    }

}