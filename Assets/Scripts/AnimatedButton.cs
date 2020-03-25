using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private Animator animator;

    void Awake () {
        animator = this.GetComponent<Animator> ();
    }

    public void OnPointerEnter (PointerEventData eventData) {
        print("playing");
        animator.SetBool ("MouseOver", true);
    }

    public void OnPointerExit (PointerEventData eventData) {
        print("stopping");
        animator.SetBool("MouseOver", false);
    }

}