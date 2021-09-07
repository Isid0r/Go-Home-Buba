using UnityEngine;

public class ShooterAmmoLogic : MonoBehaviour
{
    [SerializeField] private int _speed = 100;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * _speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
