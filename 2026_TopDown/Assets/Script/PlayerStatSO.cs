using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerStat",
    menuName = "Game/Player Stat"
)]
public class PlayerStatSO : ScriptableObject
{
    [Header("기본 공격력")]
    public int baseAttack = 1;

    [Header("최대 체력")]
    public int maxHp = 10;

    [Header("강화 비용")]
    public int upgradeCost = 5;
}