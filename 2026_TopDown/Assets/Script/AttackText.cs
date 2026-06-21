using UnityEngine;
using TMPro;

public class AttackText : MonoBehaviour
{
    public TMP_Text textUI;

    void Update()
    {
        if (
            GameDataManager
            .Instance
            != null
        )
        {
            textUI.text =
                "공격력 : "
                +
                GameDataManager
                .Instance
                .GetAttack();
        }
    }
}