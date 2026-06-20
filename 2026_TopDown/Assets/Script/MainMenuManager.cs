using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            "MainSquare"
        );
    }

    public void ExitGame()
    {
        Application.Quit();

        Debug.Log(
            "게임 종료"
        );
    }
}