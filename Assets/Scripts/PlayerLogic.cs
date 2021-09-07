using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] Text _textCherryCollected;
    [SerializeField] Image _imageKey;
    [SerializeField] Image _imageMaze;
    [SerializeField] GameObject _cherries;

    [SerializeField] GameObject _mazeObject;
    [SerializeField] GameObject _levelObject;

    private bool _isKeyFounded = false;
    private bool _isMazePassed = false;
    private bool _isMazeExist = false;
    private int _cherryTotal;
    private int _cherryCollected = 0;
    private int nextScene;

    private bool _inMaze = false;

    private void Start()
    {
        Advertisement.Initialize("3641655");
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        Levels.GetInstance(); // инициализация синглтона

        if (_cherries != null) _cherryTotal = _cherries.transform.childCount;
        else _cherryTotal = 0;

        _textCherryCollected.text = _cherryCollected.ToString() + "/" + _cherryTotal.ToString();
        if (_mazeObject != null) _isMazeExist = true;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Key"))
        {
            _isKeyFounded = true;
            _imageKey.enabled = true;
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Death());
        }

        if (col.gameObject.CompareTag("Door") && _isKeyFounded)
        {
            Levels.instance.LevelPassed(nextScene - 2, _cherryCollected, _cherryTotal,_isMazeExist, _isMazePassed);
            SaveManager.SaveLevelsProgress();
            Advertisement.Show();

            if (SceneManager.GetActiveScene().buildIndex > Levels.instance.GetLevelsCount() - 2)  // проверка на предпоследний уровень
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                SceneManager.LoadScene(nextScene);
            }
        }

        if (col.gameObject.CompareTag("Cherry"))
        {
            SoundManager.PlaySound("food");
            _cherryCollected++;
            _textCherryCollected.text = _cherryCollected.ToString() + "/" + _cherryTotal.ToString();
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("Maze"))
        {
            if (!_isMazePassed && !_inMaze)
            {
                GetComponent<PlayerControl>().Move(0); // обнуляем движение перед заходом в лабиринт
                _inMaze = true;
                _levelObject.SetActive(false);
                _mazeObject.SetActive(true);
            }
            else
            {
                _inMaze = false;
            }

        }
    }
    public void MazePassed()
    {
        _isMazePassed = true;
        _imageMaze.enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Death());
        }
    }
    private IEnumerator Death()
    {
        SoundManager.PlaySound("death");
        GetComponent<PlayerControl>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);

        yield return new WaitForSeconds(0.5f);
        string lname = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(lname);
    }
}
