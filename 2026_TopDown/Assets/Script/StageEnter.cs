using UnityEngine;
using UnityEngine.SceneManagement;

public class StageEnter : MonoBehaviour
{
    [Header("이동할 씬")]
    public string stageName = "Stage_1";

    private bool entered = false;

    void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if (
            entered ||
            !other.CompareTag(
                "Player"
            )
        )
        {
            return;
        }

        entered = true;

        Time.timeScale = 1f;

        SceneManager.LoadScene(
            stageName
        );
    }
}