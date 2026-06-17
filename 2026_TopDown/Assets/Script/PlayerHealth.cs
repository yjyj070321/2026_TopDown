using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("체력")]
    public int maxHp = 10;

    private int currentHp;

    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        Debug.Log("플레이어 체력 : " + currentHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("플레이어 사망");

        Destroy(gameObject);
    }

    public int GetHp()
    {
        return currentHp;
    }

    public void Heal(int amount)
    {
        currentHp += amount;

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }
}