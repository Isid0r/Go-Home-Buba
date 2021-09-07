using UnityEngine;

public class MazePlayerControl : MonoBehaviour
{
    private int _speed = 12;
    private Rigidbody2D rb;

    [SerializeField] GameObject _mazeObject;
    [SerializeField] GameObject _levelObject;
    [SerializeField] PlayerLogic _player;
    [SerializeField] PauseMenuMaze  _pauseMenuMaze;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _pauseMenuMaze.openPauseMenuMaze();
    }
    void Update()
    {
        rb.velocity = Vector2.zero;
        Vector3 acceleration = Input.acceleration;
        rb.velocity = new Vector2(_speed * acceleration.x, _speed * acceleration.y); //движение
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MazeExit"))
        {
            _levelObject.SetActive(true);
            _mazeObject.SetActive(false);
            _player.MazePassed();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MazeWall"))
        {
            transform.position = new Vector3(0.5f, 0.5f, 0);
            _pauseMenuMaze.openPauseMenuMaze();
        }
    }
}
