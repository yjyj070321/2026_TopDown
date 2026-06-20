using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("체력")]
    public int maxHp = 10;

    [Header("피격 효과")]
    public float hitFlashTime = 0.12f;

    [Header("사망 UI")]
    public GameObject deathPanel;

    private int currentHp;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHp = maxHp;

        if (deathPanel != null)
            deathPanel.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp < 0)
            currentHp = 0;

        Debug.Log(
            "현재 체력 : "
            + currentHp
            + "/"
            + maxHp
        );

        StartCoroutine(
            DamageFlash()
        );

        if (currentHp <= 0)
        {
            StartCoroutine(
                DieCoroutine()
            );
        }
    }

    IEnumerator DamageFlash()
    {
        if (sr != null)
        {
            sr.color =
                new Color(
                    1f,
                    0.4f,
                    0.4f
                );
        }

        yield return
            new WaitForSeconds(
                hitFlashTime
            );

        if (sr != null)
        {
            sr.color =
                Color.white;
        }
    }

    IEnumerator DieCoroutine()
    {
        yield return null;

        Debug.Log(
            "플레이어 사망"
        );

        if (deathPanel != null)
        {
            deathPanel.SetActive(
                true
            );
        }

        if (sr != null)
        {
            sr.enabled =
                false;
        }

        if (rb != null)
        {
            rb.linearVelocity =
                Vector2.zero;
        }

        Time.timeScale =
            0f;
    }

    public void Retry()
    {
        Time.timeScale =
            1f;

        SceneManager.LoadScene(
            "MainSquare"
        );
    }

    public int GetHp()
    {
        return currentHp;
    }

    public int GetMaxHp()
    {
        return maxHp;
    }

    public void Heal(
        int amount
    )
    {
        currentHp += amount;

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }
}