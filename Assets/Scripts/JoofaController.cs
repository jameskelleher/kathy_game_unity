using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoofaController : MonoBehaviour {

    Animator animator;

    void Start () {
        animator = GetComponent<Animator> ();
        NextAnimation ();
    }    

    void NextAnimation () {
        StartCoroutine ("NextAnimationCoroutine");
    }

    IEnumerator NextAnimationCoroutine () {
        print("prepping animation");
        yield return new WaitForSeconds (1f);
        float rand = Random.Range (0f, 1f);
        if (rand < 0.5f) {
            animator.Play ("joofa_spin");
        } else {
            animator.Play("joofa_bounce");
        }
    }
}