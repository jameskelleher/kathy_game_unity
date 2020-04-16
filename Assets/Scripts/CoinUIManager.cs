using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIManager : MonoBehaviour {

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
        if (coinCount > -1) {
            coinText.text = "Coins: " + coinCount;
        } else {
            coinText.text = "";
        }
    }
}