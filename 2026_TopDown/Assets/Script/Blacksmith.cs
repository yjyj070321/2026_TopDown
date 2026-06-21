using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Blacksmith : MonoBehaviour
{
    [Header("강화 UI")]
    public GameObject upgradePanel;

    public TMP_Text costText;

    [Header("대장장이 애니메이션")]
    public Sprite[] sprites;

    public float frameTime = 0.2f;

    private SpriteRenderer sr;

    private int frameIndex;

    private float timer;

    private bool playerInside;

    void Awake()
    {
        sr =
            GetComponent<
                SpriteRenderer
            >();

        if (
            sprites != null
            &&
            sprites.Length > 0
        )
        {
            sr.sprite =
                sprites[0];
        }
    }

    void Start()
    {
        if (
            upgradePanel != null
        )
        {
            upgradePanel
                .SetActive(
                    false
                );
        }
    }

    void Update()
    {
        Animate();

        if (
            playerInside
            &&
            Keyboard.current
            .eKey
            .wasPressedThisFrame
        )
        {
            if (
                upgradePanel
                .activeSelf
            )
            {
                CloseUI();
            }
            else
            {
                OpenUI();
            }
        }
    }

    void Animate()
    {
        if (
            sprites == null
            ||
            sprites.Length <= 1
        )
        {
            return;
        }

        timer +=
            Time.unscaledDeltaTime;

        if (
            timer >= frameTime
        )
        {
            timer = 0;

            frameIndex++;

            if (
                frameIndex
                >=
                sprites.Length
            )
            {
                frameIndex = 0;
            }

            sr.sprite =
                sprites[
                    frameIndex
                ];
        }
    }

    void OpenUI()
    {
        upgradePanel
            .SetActive(
                true
            );

        Time.timeScale =
            0f;

        costText.text =
            "강화 비용 : 5 골드";
    }

    public void Upgrade()
    {
        GameDataManager
            .Instance
            .UpgradeAttack();
    }

    public void CloseUI()
    {
        upgradePanel
            .SetActive(
                false
            );

        Time.timeScale =
            1f;
    }

    void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if (
            other
            .CompareTag(
                "Player"
            )
        )
        {
            playerInside =
                true;
        }
    }

    void OnTriggerExit2D(
        Collider2D other
    )
    {
        if (
            other
            .CompareTag(
                "Player"
            )
        )
        {
            playerInside =
                false;

            CloseUI();
        }
    }
}