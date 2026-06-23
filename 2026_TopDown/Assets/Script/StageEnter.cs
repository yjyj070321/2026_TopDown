using UnityEngine;
using UnityEngine.SceneManagement;

public class StageEnter : MonoBehaviour
{
[Header("이동할 씬")]
public string stageName =
"Stage_1";

[Header("클리어 화면 사용")]
public bool showClearPanel;

public GameObject clearPanel;

[Header("메뉴 씬 이름")]
public string menuSceneName =
    "Menu";

bool entered;

void OnTriggerEnter2D(
    Collider2D other
)
{
    if (
        entered
        ||
        !other.CompareTag(
            "Player"
        )
    )
    {
        return;
    }

    entered =
        true;

    if (
        showClearPanel
    )
    {
        Time.timeScale =
            0f;

        if (
            clearPanel != null
        )
        {
            clearPanel.SetActive(
                true
            );
        }
    }
    else
    {
        Time.timeScale =
            1f;

        SceneManager.LoadScene(
            stageName
        );
    }
}

public void Retry()
{
    Time.timeScale =
        1f;

    SceneManager.LoadScene(
        SceneManager
        .GetActiveScene()
        .name
    );
}

public void GoMenu()
{
    Time.timeScale =
        1f;

    SceneManager.LoadScene(
        menuSceneName
    );
}

}
