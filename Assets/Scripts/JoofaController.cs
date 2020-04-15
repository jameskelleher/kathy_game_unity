using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoofaController : MonoBehaviour {
    Animator animator;

    void Start () {
        animator = GetComponent<Animator>();
        SpinAnimation();
    }

    void SpinAnimation() {
        StartCoroutine("SpinAnimationCoroutine");
    }

    IEnumerator SpinAnimationCoroutine () {
        yield return new WaitForSeconds (1f);
        animator.Play("joofa_spin");
    }
}