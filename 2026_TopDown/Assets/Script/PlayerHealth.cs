using UnityEngine;
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

    private Vector3 spawnPosition;
    private Rigidbody2D rb;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHp = maxHp;

        // 처음 스폰 위치 저장
        spawnPosition = transform.position;

        if (deathPanel != null)
            deathPanel.SetActive(false);
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
            sr.color = new Color(1f, 0.4f, 0.4f);

        yield return new WaitForSeconds(hitFlashTime);

        if (sr != null)
            sr.color = Color.white;
    }

    IEnumerator DieCoroutine()
    {
        yield return null;

        Debug.Log("플레이어 사망");

        // 사망 창 표시
        if (deathPanel != null)
            deathPanel.SetActive(true);

        // 플레이어 숨김 (카메라는 유지)
        if (sr != null)
            sr.enabled = false;

        // 이동 멈춤
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // 게임 정지
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        // 게임 재개
        Time.timeScale = 1f;

        // 체력 회복
        currentHp = maxHp;

        // 위치 초기화
        transform.position = spawnPosition;

        // 플레이어 다시 표시
        if (sr != null)
        {
            sr.enabled = true;
            sr.color = Color.white;
        }

        // UI 닫기
        if (deathPanel != null)
            deathPanel.SetActive(false);

        Debug.Log("리스폰 완료");
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