using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("체력")]
    public int maxHp = 10;

    [Header("피격 효과")]
    public float hitFlashTime = 0.12f;

    private int currentHp;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp < 0)
            currentHp = 0;

        Debug.Log("현재 체력 : " + currentHp + "/" + maxHp);

        StartCoroutine(DamageFlash());

        if (currentHp <= 0)
        {
            StartCoroutine(DieCoroutine());
        }
    }

    IEnumerator DamageFlash()
    {
        if (sr != null)
        {
            sr.color = new Color(1f, 0.4f, 0.4f);
        }

        yield return new WaitForSeconds(hitFlashTime);

        if (sr != null)
        {
            sr.color = Color.white;
        }
    }

    IEnumerator DieCoroutine()
    {
        // 체력바가 0으로 갱신될 시간 1프레임 줌
        yield return null;

        Debug.Log("플레이어 사망");
        Destroy(gameObject);
    }

    public int GetHp()
    {
        return currentHp;
    }

    public int GetMaxHp()
    {
        return maxHp;
    }

    public void Heal(int amount)
    {
        currentHp += amount;

        if (currentHp > maxHp)
            currentHp = maxHp;
    }
}