using UnityEngine;

public class MoveSomething : MonoBehaviour
{
    [Header("Для выбора режима: 0 - линия, 1 - круг.")]
    [SerializeField] private uint _mode;
    [SerializeField] private float _speed = 2;
    [Header("Значения для движения по линии/синусоиды")]
    [SerializeField] Transform _position1;
    [SerializeField] Transform _position2;
    [Header("Значения для движения по кругу.")]
    [SerializeField] Transform _positionCenterCircle;
    [SerializeField] private Vector2 _vecCoefCircle; // влияет на начало и сторону движения по кругу
    [SerializeField] private float _radiusCircle = 2f;

    private float _angleCircle = 0; // начальный угол для вычислений
    Vector3 _nextPosition;
    void Start()
    {
        _nextPosition = _position1.position;
    }
    void Update()
    {
        switch (_mode)
        {
            case 0:
                LineMovement();
                break;
            case 1:
                CircleMovement();
                break;
            default:
                break;
        }
    }
    void CircleMovement()
    {
        CircleCheck360();      
        transform.position = _positionCenterCircle.position + CircleFindCoordinates();
    }
    void CircleCheck360()
    {
        if (_angleCircle >= 360) _angleCircle = 0;
    }
    Vector3 CircleFindCoordinates()
    {
        _angleCircle = _angleCircle + _speed * Time.deltaTime;
        var x = Mathf.Cos(_angleCircle) * _radiusCircle * _vecCoefCircle.x;
        var y = Mathf.Sin(_angleCircle) * _radiusCircle * _vecCoefCircle.y;
        return new Vector3(x, y, 0);
    }
    void LineMovement()
    {
        LineCheckPosition();
        transform.position = Vector3.MoveTowards(transform.position, _nextPosition, _speed * Time.deltaTime);
    }
    void LineCheckPosition()
    {
        if (transform.position == _position1.position)
        {
            _nextPosition = _position2.position;
        }
        if (transform.position == _position2.position)
        {
            _nextPosition = _position1.position;
        }
    }
    private void OnDrawGizmos()
    {
        if (((_mode == 0) || (_mode == 2)) && (_position1 != null) && (_position2 != null))
        {
            Gizmos.DrawLine(_position1.position, _position2.position);
        }
    }
}