using UnityEngine;

public class PauseMenuMaze : MonoBehaviour
{
    [SerializeField] GameObject _mazeObject;
    [SerializeField] GameObject _levelObject;

    public void openPauseMenuMaze()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }
    public void StartPlayMaze()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void GiveUp()
    {
        Time.timeScale = 1f;
        _levelObject.SetActive(true);
        _mazeObject.SetActive(false);
    }

}
