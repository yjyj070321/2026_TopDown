using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("코인 개수")]
    public int value = 1;

    [Header("애니메이션")]
    public Sprite[] coinSprites;
    public float frameTime = 0.12f;

    private SpriteRenderer sr;

    private int frameIndex = 0;
    private float timer = 0f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        if (coinSprites.Length > 0)
            sr.sprite = coinSprites[0];
    }

    void Update()
    {
        if (coinSprites.Length <= 1)
            return;

        timer += Time.deltaTime;

        if (timer >= frameTime)
        {
            timer = 0f;

            frameIndex++;

            if (frameIndex >= coinSprites.Length)
                frameIndex = 0;

            sr.sprite = coinSprites[frameIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.instance.AddCoin(value);

            Destroy(gameObject);
        }
    }
}