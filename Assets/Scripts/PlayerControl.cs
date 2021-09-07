using UnityEngine;
public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;

    float _jumpPressedRemember = 0;
    [SerializeField]  float _jumpPressedRememberTime = 0.1f;
    float _groundedRemember = 0;
    [SerializeField]  float _groundedRememberTime = 0.2f;

    [SerializeField] private LayerMask _mask;

    int _directionInput = 0;
    bool _jumpPressed = false;
    bool _faceRight = true;
    bool _isGrounded;
    bool _isJumpButtonUp = false;

    [SerializeField] float _jumpPower = 17;
    [SerializeField]  float _speed = 6;
    [SerializeField] float _jumpTimeCounter = 1; // мультипликатор для высоты прыжка

    Transform _platform;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        //Debug.DrawRay(new Vector2(boxCollider2D.bounds.center.x, boxCollider2D.bounds.center.y - (boxCollider2D.bounds.size.y/2)), new Vector2(boxCollider2D.bounds.size.x/2, 0));
        DoIfOnMovingPlatform();
        JumpController();
        Flip();
        rb.velocity = new Vector2(_speed * _directionInput, rb.velocity.y); //движение
    }
    void JumpController()
    {
        RegulateJumpHeight();
        CheckGrounded();
        CheckPressJumpButton();
        DoJump();
    }
    void DoJump()
    {
        if ((_jumpPressedRemember > 0) && (_groundedRemember > 0))
        {
            _groundedRemember = 0;
            _jumpPressedRemember = 0;
            rb.AddForce((Vector2.up * _jumpPower), ForceMode2D.Impulse);
            _jumpTimeCounter = 1;

            SoundManager.PlaySound("jump"); // звук  прыжка
        }
    }
    void CheckPressJumpButton()  // Персонаж прыгает, даже если кнопка была нажата за некоторое время до приземления
    {
        if (_jumpPressedRemember >= 0) _jumpPressedRemember -= Time.fixedDeltaTime;
        if (_jumpPressed)
        {
            _jumpPressed = false;
            _jumpPressedRemember = _jumpPressedRememberTime;
        }
    }
    void CheckGrounded()  // задержка проверки на нахождение на земле
    {
        _isGrounded = Physics2D.OverlapBox(new Vector2(boxCollider2D.bounds.center.x, boxCollider2D.bounds.center.y - (boxCollider2D.bounds.size.y / 2)), new Vector2((boxCollider2D.bounds.size.x) - 0.03f, 0), 0f, _mask);
        if (_groundedRemember >= 0) _groundedRemember -= Time.fixedDeltaTime;
        if (_isGrounded)
        {
            _groundedRemember = _groundedRememberTime;
        }
    }
    void RegulateJumpHeight()
    {
        if (_jumpTimeCounter > 0) _jumpTimeCounter -= Time.fixedDeltaTime;
        if (_isJumpButtonUp)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * _jumpTimeCounter * 0.7f);
            }
            _isJumpButtonUp = false;
        }
    }
    void Flip()
    {
        if ((_directionInput > 0) && (!_faceRight))
        {
            transform.Rotate(0, 180, 0);
            _faceRight = !_faceRight;
        }
        if ((_directionInput < 0) && (_faceRight))
        {
            transform.Rotate(0, 180, 0);
            _faceRight = !_faceRight;
        }
    }
    public void Move(int InputAxis)
    {
        _directionInput = InputAxis;
    }
    private void DoIfOnMovingPlatform()
    {
        if ((_directionInput != 0) && (_platform != null))
        {
            transform.parent = null;
        }
        if ((_directionInput == 0) && (_platform != null))
        {
            transform.parent = _platform.transform;
        }
    }
    public void Jump(bool jumpPressed)
    {
        if (_jumpPressed)
            _jumpPressed = jumpPressed;
        else
        {
            _jumpPressed = jumpPressed;
            _isJumpButtonUp = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = coll.gameObject.transform;
            _platform = coll.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("MovingPlatform"))
        {
            _platform = null;
            transform.parent = null;
        }
    }

}
