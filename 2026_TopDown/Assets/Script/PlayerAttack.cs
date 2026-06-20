using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public Sprite[] attackUp;
    public Sprite[] attackDown;
    public Sprite[] attackLeft;
    public Sprite[] attackRight;

    public float frameTime = 0.08f;

    public GameObject swordHitbox;

    private SpriteRenderer sr;
    private PlayerController movement;

    private bool isAttacking = false;

    public static int CurrentDamage
    {
        get
        {
            if (GameDataManager.Instance != null)
            {
                return GameDataManager
                    .Instance
                    .GetAttack();
            }

            return 1;
        }
    }

    private void Awake()
    {
        sr =
            GetComponent<
                SpriteRenderer
            >();

        movement =
            GetComponent<
                PlayerController
            >();

        if (
            swordHitbox != null
        )
        {
            swordHitbox
                .SetActive(
                    false
                );
        }
    }

    public void OnAttack(
        InputValue value
    )
    {
        // UI 클릭 중이면 공격 금지
        if (
            EventSystem.current
            != null
            &&
            EventSystem
            .current
            .IsPointerOverGameObject()
        )
        {
            return;
        }

        // 강화창 열렸을 때 공격 금지
        if (
            Time.timeScale
            == 0f
        )
        {
            return;
        }

        if (
            !isAttacking
        )
        {
            StartCoroutine(
                Attack()
            );
        }
    }

    IEnumerator Attack()
    {
        isAttacking =
            true;

        UpdateHitboxPosition();

        Sprite[] attackSprites =
            GetAttackSprites();

        for (
            int i = 0;
            i < attackSprites.Length;
            i++
        )
        {
            sr.sprite =
                attackSprites[i];

            if (
                i == 1
            )
            {
                swordHitbox
                    .SetActive(
                        true
                    );
            }

            yield return
                new WaitForSeconds(
                    frameTime
                );
        }

        swordHitbox
            .SetActive(
                false
            );

        isAttacking =
            false;
    }

    Sprite[] GetAttackSprites()
    {
        Vector2 dir =
            movement
            .GetDirection();

        if (
            Mathf.Abs(
                dir.x
            )
            >
            Mathf.Abs(
                dir.y
            )
        )
        {
            return
                dir.x > 0
                ?
                attackRight
                :
                attackLeft;
        }

        return
            dir.y > 0
            ?
            attackUp
            :
            attackDown;
    }

    void UpdateHitboxPosition()
    {
        Vector2 dir =
            movement
            .GetDirection();

        float xOffset =
            0.35f;

        float yOffset =
            0.25f;

        if (
            Mathf.Abs(
                dir.x
            )
            >
            Mathf.Abs(
                dir.y
            )
        )
        {
            swordHitbox
                .transform
                .localPosition =
                new Vector3(
                    dir.x > 0
                    ?
                    xOffset
                    :
                    -xOffset,
                    -0.05f,
                    0f
                );
        }
        else
        {
            swordHitbox
                .transform
                .localPosition =
                new Vector3(
                    0f,
                    dir.y > 0
                    ?
                    yOffset
                    :
                    -yOffset,
                    0f
                );
        }
    }

    public bool IsAttacking()
    {
        return
            isAttacking;
    }
}