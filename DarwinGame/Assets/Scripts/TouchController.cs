using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    // References
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private bool _isImmune;
    private float immuneTimeCount = 0;
    private float immuneTime = 0.5f;

    // Movement
    private Vector2 _movement;
    private bool _facingRight = true;
    private bool _isGrounded;

    // Inspector variables
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float hurtForce = 10f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        //_movement = new Vector2(horizontalInput, 0f);

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToViewportPoint(touch.position);

            if (touchPosition.x > 0 && _facingRight == true)
            {
                float horizontalVelocity = _movement.normalized.x * speed;
                _rigidbody.velocity = new Vector2(horizontalVelocity, 0f);
                Flip();
                Debug.Log("Hola derechaa");
            } else if (touchPosition.x < 0 && _facingRight == false)
            {
                float horizontalVelocity = _movement.normalized.x * speed;
                _rigidbody.velocity = new Vector2(horizontalVelocity, 0f);
                Flip();
            }
        }

        // Flip character
        //if (horizontalInput < 0f && _facingRight == true)
        //{
        //    Flip();
        //}
        //else if (horizontalInput > 0f && _facingRight == false)
        //{
        //    Flip();
        //}

        // Is Grounded?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Is Jumping?
        if (Input.GetButtonDown("Jump") && _isGrounded == true)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Is Immune?
        if (_isImmune)
        {
            _sprite.enabled = !_sprite.enabled;
            immuneTimeCount -= Time.deltaTime;

            if (immuneTimeCount <= 0)
            {
                _isImmune = false;
                _sprite.enabled = true;
            }
        }
    }

    //void FixedUpdate()
    //{
    //    float horizontalVelocity = _movement.normalized.x * speed;
    //    _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
    //}


    void LateUpdate()
    {
        _animator.SetBool("idle", _movement == Vector2.zero);
        _animator.SetBool("isGrounded", _isGrounded);
        _animator.SetFloat("verticalVelocity", _rigidbody.velocity.y);
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX *= -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
