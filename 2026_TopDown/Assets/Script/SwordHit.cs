using UnityEngine;

public class SwordHit : MonoBehaviour
{
private void OnTriggerEnter2D(
Collider2D other
)
{
if (
other.CompareTag(
"Enemy"
)
)
{
Enemy enemy =
other.GetComponent<
Enemy
>();

        if (
            enemy != null
        )
        {
            enemy.TakeDamage(
                PlayerAttack
                .CurrentDamage
            );

            Debug.Log(
                "공격력 : "
                +
                PlayerAttack
                .CurrentDamage
            );
        }
    }
}

}