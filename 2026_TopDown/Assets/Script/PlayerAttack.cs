using UnityEngine;
using UnityEngine.InputSystem;
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

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        movement = GetComponent<PlayerController>();

        swordHitbox.SetActive(false);
    }

    public void OnAttack(InputValue value)
    {
        if (!isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        UpdateHitboxPosition();

        Sprite[] attackSprites = GetAttackSprites();

        for (int i = 0; i < attackSprites.Length; i++)
        {
            sr.sprite = attackSprites[i];

            if (i == 1)
                swordHitbox.SetActive(true);

            yield return new WaitForSeconds(frameTime);
        }

        swordHitbox.SetActive(false);

        isAttacking = false;
    }

    Sprite[] GetAttackSprites()
    {
        Vector2 dir = movement.GetDirection();

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            return dir.x > 0
                ? attackRight
                : attackLeft;
        }
        else
        {
            return dir.y > 0
                ? attackUp
                : attackDown;
        }
    }

    void UpdateHitboxPosition()
    {
        Vector2 dir = movement.GetDirection();

        float xOffset = 0.35f;
        float yOffset = 0.25f;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                swordHitbox.transform.localPosition =
                    new Vector3(xOffset, -0.05f, 0f);
            }
            else
            {
                swordHitbox.transform.localPosition =
                    new Vector3(-xOffset, -0.05f, 0f);
            }
        }
        else
        {
            if (dir.y > 0)
            {
                swordHitbox.transform.localPosition =
                    new Vector3(0f, yOffset, 0f);
            }
            else
            {
                swordHitbox.transform.localPosition =
                    new Vector3(0f, -yOffset, 0f);
            }
        }
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}
