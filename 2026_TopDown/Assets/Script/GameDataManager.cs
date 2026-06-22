using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public float volume = 1f;

    public bool BGM = true;

    // 코인
    public int gold = 0;

    // 현재 공격력
    public int attack = 1;
}

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    public PlayerData playerData;

    // ScriptableObject 연결
    public PlayerStatSO playerStat;

    void Awake()
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

            return
                JsonUtility
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
        playerData.gold =
            amount;

        SaveData(
            playerData
        );
    }

    public int GetGold()
    {
        return
            playerData.gold;
    }

    public int GetAttack()
    {
        if (
            playerData.attack <= 0
        )
        {
            return
                playerStat
                .baseAttack;
        }

        return
            playerData.attack;
    }

    public void UpgradeAttack()
    {
        if (
            playerData.gold
            <
            playerStat
            .upgradeCost
        )
        {
            Debug.Log(
                "골드 부족"
            );

            return;
        }

        playerData.gold -=
            playerStat
            .upgradeCost;

        playerData.attack +=
            1;

        SaveData(
            playerData
        );

        if (
            CoinManager
            .instance
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
        playerData.gold =
            0;

        if (
            playerStat
            != null
        )
        {
            playerData.attack =
                playerStat
                .baseAttack;
        }
        else
        {
            playerData.attack =
                1;
        }

        SaveData(
            playerData
        );

        Debug.Log(
            "게임 종료 → 골드 / 공격력 초기화"
        );
    }
}