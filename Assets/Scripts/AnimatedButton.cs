using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public float waitToAppear;

    public float secondsToAppear;

    Animator animator;

    Button button;

    Image image;

    void Awake () {
        animator = this.GetComponent<Animator> ();
        button = this.GetComponent<Button> ();
        image = this.GetComponent<Image> ();

        button.interactable = false;

        Color c = image.color;
        c.a = 0f;
        image.color = c;

        IEnumerator coroutine = StartUpCoroutine ();
        StartCoroutine (coroutine);
    }

    IEnumerator StartUpCoroutine () {
        yield return new WaitForSeconds (waitToAppear);

        button.interactable = true;

        while (image.color.a < 1) {
            Color c = image.color;
            c.a += Time.deltaTime / secondsToAppear;
            print (c.a);
            image.color = c;
            yield return null;
        }

    }

    public void OnPointerEnter (PointerEventData eventData) {
        animator.SetBool ("MouseOver", true);
    }

    public void OnPointerExit (PointerEventData eventData) {
        animator.SetBool ("MouseOver", false);
    }

}