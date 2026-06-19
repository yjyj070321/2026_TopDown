using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
public static CoinManager instance;

[Header("코인 UI")]
public TMP_Text coinText;

private int coinCount = 0;

void Awake()
{
    instance = this;
}

void Start()
{
    UpdateUI();
}

public void AddCoin(int amount)
{
    coinCount += amount;

    UpdateUI();

    Debug.Log("현재 코인 : " + coinCount);
}

void UpdateUI()
{
    if (coinText != null)
    {
        coinText.text = "코인 : " + coinCount;
    }
}

public int GetCoin()
{
    return coinCount;
}

}
