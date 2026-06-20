using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public TMP_Text coinText;

    private int coinCount;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        coinCount =
            GameDataManager
            .Instance
            .GetGold();

        UpdateUI();
    }

    public void AddCoin(
        int amount
    )
    {
        coinCount += amount;

        GameDataManager
            .Instance
            .SetGold(
                coinCount
            );

        UpdateUI();
    }

    public void Refresh()
    {
        coinCount =
            GameDataManager
            .Instance
            .GetGold();

        UpdateUI();
    }

    void UpdateUI()
    {
        if (
            coinText != null
        )
        {
            coinText.text =
                "코인 : "
                +
                coinCount;
        }
    }

    public int GetCoin()
    {
        return coinCount;
    }
}