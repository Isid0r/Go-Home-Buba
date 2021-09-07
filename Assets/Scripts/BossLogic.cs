using UnityEngine;

public class BossLogic : MonoBehaviour
{
    [SerializeField] int _speed = 15;
    [SerializeField] Transform _leftBorderCamera;
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * Time.fixedDeltaTime * _speed);
        _leftBorderCamera.position += Vector3.right * Time.fixedDeltaTime * _speed;
    }
}
