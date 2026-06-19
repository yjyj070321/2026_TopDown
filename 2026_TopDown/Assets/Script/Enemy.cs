using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("이동")]
    public float moveSpeed = 2f;

    [Header("플레이어 감지 거리")]
    public float detectDistance = 5f;

    [Header("붙는 거리")]
    public float stopDistance = 0.08f;

    [Header("피격")]
    public float knockbackPower = 4f;
    public float knockbackTime = 0.15f;

    [Header("방향 이미지")]
    public Sprite[] spriteUp;
    public Sprite[] spriteDown;
    public Sprite[] spriteLeft;
    public Sprite[] spriteRight;

    public float frameTime = 0.15f;

    [Header("체력")]
    public int maxHp = 3;

    [Header("접촉 데미지")]
    public int touchDamage = 1;
    public float attackCooldown = 1f;

    private Transform player;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 moveDir;

    private int currentHp;

    private Sprite[] currentSprites;

    private int frameIndex;

    private float animationTimer;

    private float damageTimer;

    private bool isKnockback;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        currentSprites = spriteDown;

        if (currentSprites != null &&
            currentSprites.Length > 0)
        {
            sr.sprite = currentSprites[0];
        }
    }

    void Start()
    {
        currentHp = maxHp;

        GameObject p =
            GameObject.FindGameObjectWithTag("Player");

        if (p != null)
        {
            player = p.transform;
        }
    }

    void FixedUpdate()
    {
        if (isKnockback)
            return;

        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 toPlayer =
            player.position -
            transform.position;

        float distance =
            toPlayer.magnitude;

        // 감지 범위 밖
        if (distance > detectDistance)
        {
            moveDir =
                Vector2.zero;

            rb.linearVelocity =
                Vector2.zero;

            return;
        }

        // 너무 가까우면 멈춤
        if (distance <= stopDistance)
        {
            moveDir =
                Vector2.zero;

            rb.linearVelocity =
                Vector2.zero;

            return;
        }

        moveDir =
            toPlayer.normalized;

        rb.linearVelocity =
            moveDir *
            moveSpeed;
    }

    void Update()
    {
        damageTimer -= Time.deltaTime;

        UpdateDirection();

        Animate();
    }

    void UpdateDirection()
    {
        if (moveDir.sqrMagnitude < 0.01f)
            return;

        if (Mathf.Abs(moveDir.x) >
            Mathf.Abs(moveDir.y))
        {
            ChangeSprites(
                moveDir.x > 0
                ? spriteRight
                : spriteLeft
            );
        }
        else
        {
            ChangeSprites(
                moveDir.y > 0
                ? spriteUp
                : spriteDown
            );
        }
    }

    void Animate()
    {
        if (currentSprites == null)
            return;

        if (currentSprites.Length <= 1)
            return;

        animationTimer += Time.deltaTime;

        if (animationTimer >= frameTime)
        {
            animationTimer = 0;

            frameIndex++;

            if (frameIndex >= currentSprites.Length)
            {
                frameIndex = 0;
            }

            sr.sprite =
                currentSprites[frameIndex];
        }
    }

    void ChangeSprites(Sprite[] newSprites)
    {
        if (currentSprites == newSprites)
            return;

        currentSprites =
            newSprites;

        frameIndex = 0;

        if (currentSprites.Length > 0)
        {
            sr.sprite =
                currentSprites[0];
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        StartCoroutine(
            DamageEffect()
        );

        if (player != null)
        {
            Vector2 dir =
                (
                    transform.position -
                    player.position
                ).normalized;

            StartCoroutine(
                Knockback(dir)
            );
        }

        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DamageEffect()
    {
        sr.color =
            new Color(
                1f,
                0.4f,
                0.4f
            );

        yield return
            new WaitForSeconds(
                0.12f
            );

        sr.color =
            Color.white;
    }

    IEnumerator Knockback(
        Vector2 dir
    )
    {
        isKnockback = true;

        rb.linearVelocity =
            dir *
            knockbackPower;

        yield return
            new WaitForSeconds(
                knockbackTime
            );

        rb.linearVelocity =
            Vector2.zero;

        isKnockback = false;
    }

    void OnTriggerStay2D(
        Collider2D other
    )
    {
        if (!other.CompareTag("Player"))
            return;

        if (damageTimer <= 0)
        {
            PlayerHealth hp =
                other.GetComponent<PlayerHealth>();

            if (hp != null)
            {
                hp.TakeDamage(
                    touchDamage
                );
            }

            damageTimer =
                attackCooldown;
        }
    }
}