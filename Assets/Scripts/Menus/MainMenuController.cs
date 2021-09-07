using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button[] _lvlButtons;
    private List<Text> _info = new List<Text>();
    private List<Image> _cherries = new List<Image>();
    private List<Image> _mazes = new List<Image>();

    void Awake()
    {
        Levels.GetInstance();
        //DeleteSave();
        SaveManager.LoadLevelsProgress();
        foreach (var item in _lvlButtons)
        {
            _info.Add(item.transform.Find("ProgressText").gameObject.GetComponent<Text>());
            _cherries.Add(item.transform.Find("CherryImage").gameObject.GetComponent<Image>());
            _mazes.Add(item.transform.Find("MazeImage").gameObject.GetComponent<Image>());
        }
    }
    void Start()
    {
        InitializeButtons();
    }
    void InitializeButtons()
    {
        Levels.instance.OpenLevel(0);
        for (int i = 0; i < _lvlButtons.Length; i++)
        {
            _lvlButtons[i].interactable = Levels.instance.CheckLevelAvailable(i);
            int totCher = Levels.instance.GetCherriesTotal(i);
            if (totCher != 0)
            {
                _info[i].text = Levels.instance.GetCherriesCollected(i).ToString() + " / " + totCher.ToString();
                _cherries[i].enabled = true;
            }
            else 
            {
                _info[i].text = "";
                _cherries[i].enabled = false;
            }
            if (Levels.instance.IsMazePassed(i)) _mazes[i].enabled = true;
            else _mazes[i].enabled = false;
        }
    }
    public void DeleteSave()
    {
        Levels.instance.ClearProgress();
        SaveManager.SaveLevelsProgress();
        InitializeButtons();
    }
    public void OpenLevel(int numLevel)
    {
        SceneManager.LoadScene(numLevel);
    }
}
