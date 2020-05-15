using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    public float secondsToFade = 0.5f;

    Image image;

    float alpha;

    void Awake () {
        image = this.gameObject.GetComponent<Image> ();
        Color color = image.color;
        alpha = 1.0f;
        color.a = alpha;
        image.color = color;
    }

    void Update () {
        if (alpha > 0f) {
            alpha -= Time.deltaTime / secondsToFade;
            Color color = image.color;
            color.a = alpha;
            image.color = color;

            if (alpha <= 0) GameManager.Instance.SetCanMove(true);
        }
    }
}