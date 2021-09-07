using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isLevelPaused = false;
    [SerializeField] private GameObject PausePanel;
    public void OpenPauseMenu()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isLevelPaused = true;
    }
    public void ClosePauseMenu()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        isLevelPaused = false;
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        isLevelPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        isLevelPaused = false;
        SceneManager.LoadScene(0);
    }
}
