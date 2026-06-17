using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image fillImage;

    void LateUpdate()
    {
        if (playerHealth == null || fillImage == null)
            return;

        fillImage.fillAmount =
            Mathf.Clamp01(
                (float)playerHealth.GetHp() /
                playerHealth.GetMaxHp()
            );
    }
}