using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIManager : MonoBehaviour {

    public GameObject[] coinImages;

    public Text coinText;

    [HideInInspector]
    public static bool UIExists;


    void Start () {
        if (UIExists) {
            Destroy (this.gameObject);
        } else {
            UIExists = true;
            DontDestroyOnLoad (transform.gameObject);
        }
    }

    void Update () {
        int coinCount = GameManager.Instance.coinCount;
        if (coinCount > 0) {
            coinText.text = "Coins: " + coinCount;
        } else {
            coinText.text = "";
        }

        foreach (GameObject coinImage in coinImages) {
            coinImage.SetActive(false);
        }

        int imagesToActivate = Mathf.Min(coinImages.Length, coinCount);
        for (int i = 0; i < imagesToActivate; i++) {
            coinImages[i].SetActive(true);
        }
    }

}