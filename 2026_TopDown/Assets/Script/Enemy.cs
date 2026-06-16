using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 5;

    public void TakeDamage(int damage)
    {
        hp -= damage;

        Debug.Log("적 피격! 남은 체력: " + hp);

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
