using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float moveSpeed = 5;
    GameObject followTarget;
    Vector3 targetPos;

    void Update () {
        if (followTarget) {
            Vector3 followPos = followTarget.transform.position;
            this.targetPos = new Vector3(followPos.x, followPos.y, -10);
            this.transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }

    public void SetFollowTarget(GameObject followTarget) {
        this.followTarget = followTarget;
    }
}