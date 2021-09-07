using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Transform _leftBorder,_rightBorder,_topBorder,_bottomBorder;
    Vector2 _cornerLeftBottom, _cornerRightTop;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _cornerLeftBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        _cornerRightTop = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        transform.position = _player.position + new Vector3(0, 1, -1);
    }
    void Update()
    {
        transform.position = _player.position + new Vector3(0, 1, -1);
        CheckBorders();
    }
    void CheckBorders()
    {
        CheckRightBorder();
        CheckLeftBorder();
        CheckTopBorder();
        CheckBottomBorder();
    }
    void CheckLeftBorder()
    {
        if ((transform.position.x + _cornerLeftBottom.x) < _leftBorder.position.x)
        {
            transform.position = new Vector3(_leftBorder.position.x - _cornerLeftBottom.x, transform.position.y, -1);
        }
    }
    void CheckRightBorder()
    {
        if ((transform.position.x + _cornerRightTop.x) > _rightBorder.position.x)
        {
            transform.position = new Vector3(_rightBorder.position.x - _cornerRightTop.x, transform.position.y, -1);
        }
    }
    void CheckTopBorder()
    {
        if ((transform.position.y + _cornerRightTop.y) > _topBorder.position.y)
        {
            transform.position = new Vector3(transform.position.x, _topBorder.position.y - _cornerRightTop.y, -1);
        }
    }
    void CheckBottomBorder()
    {
        if ((transform.position.y + _cornerLeftBottom.y) <= _bottomBorder.position.y)
        {
            transform.position = new Vector3(transform.position.x, _bottomBorder.position.y - _cornerLeftBottom.y, -1);
        }
    }
}
