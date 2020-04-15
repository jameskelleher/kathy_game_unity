using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float moveSpeed = 5;
    GameObject followTarget;
    Vector3 targetPos;

    public BoxCollider2D boundBox;
    Vector3 minBounds;
    Vector3 maxBounds;
    Camera camera;
    float halfHeight;
    float halfWidth;

    void Start() {
        if (boundBox != null) {
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
        }
        camera = GetComponent<Camera>();
        halfHeight = camera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    void Update () {
        if (followTarget) {
            Vector3 followPos = followTarget.transform.position;
            this.targetPos = new Vector3(followPos.x, followPos.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

            float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);

        }
    }

    public void SetFollowTarget(GameObject followTarget) {
        this.followTarget = followTarget;
    }
}