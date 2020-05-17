using Photon.Pun;
using UnityEngine;


public class ExitPoint : MonoBehaviour {
    public string loadLevel;
    public string spawnPointName;
    public float setLastMoveX;
    public float setLastMoveY;

    public GameObject fadeOutManger;

    bool startedFadeout = false;


    void OnTriggerEnter2D (Collider2D other) {

        if (startedFadeout) return;

        if (other.gameObject.GetComponent<PlayerController>()) {
            PhotonView photonView = other.gameObject.GetComponent<PhotonView> ();
            if (photonView.IsMine) {
                GameManager.Instance.SetSpawnPointName(spawnPointName);
                GameManager.Instance.lastMove = new Vector2(setLastMoveX, setLastMoveY);

                fadeOutManger.GetComponent<TransitionManager>().StartFadeout(loadLevel, other);                
            }
        }
    }
}