using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public float volume = 1f;

    public bool BGM = true;

    // 코인(골드)
    public int gold = 0;

    // 공격력
    public int attack = 1;
}

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    public PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(
                gameObject
            );

            playerData =
                LoadData();
        }
        else
        {
            Destroy(
                gameObject
            );
        }
    }

    public void SaveData(
        PlayerData playerData
    )
    {
        string filePath =
            Application
            .persistentDataPath
            +
            "/player_data.json";

        string json =
            JsonUtility
            .ToJson(
                playerData,
                true
            );

        System.IO.File
            .WriteAllText(
                filePath,
                json
            );
    }

    public PlayerData LoadData()
    {
        string filePath =
            Application
            .persistentDataPath
            +
            "/player_data.json";

        if (
            System.IO.File
            .Exists(
                filePath
            )
        )
        {
            string json =
                System.IO.File
                .ReadAllText(
                    filePath
                );

            return JsonUtility
                .FromJson<PlayerData>(
                    json
                );
        }

        return new PlayerData();
    }

    public void AddGold(
        int amount
    )
    {
        playerData.gold += amount;

        SaveData(
            playerData
        );
    }

    public void SetGold(
        int amount
    )
    {
        playerData.gold = amount;

        SaveData(
            playerData
        );
    }

    public int GetGold()
    {
        return playerData.gold;
    }

    public int GetAttack()
    {
        return playerData.attack;
    }

    public void UpgradeAttack()
    {
        if (
            playerData.gold < 5
        )
        {
            Debug.Log(
                "골드 부족"
            );

            return;
        }

        playerData.gold -= 5;

        playerData.attack += 1;

        SaveData(
            playerData
        );

        if (
            CoinManager.instance
            != null
        )
        {
            CoinManager
                .instance
                .Refresh();
        }

        Debug.Log(
            "공격력 : "
            +
            playerData.attack
        );
    }

    private void OnApplicationQuit()
    {
        // 게임 종료 시 초기화
        playerData.gold = 0;

        playerData.attack = 1;

        SaveData(
            playerData
        );

        Debug.Log(
            "게임 종료 → 골드 / 공격력 초기화"
        );
    }
}