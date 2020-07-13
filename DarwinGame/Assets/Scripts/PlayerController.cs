using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Singleton
    public static PlayerController obj;

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
        obj = this;
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
        Movement();

        // Is Grounded?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Is Jumping?
        //float verticalInput = _joystick.Vertical;
        //if (Input.GetButtonDown("Jump") && _isGrounded == true) #Computer movement
        //if(verticalInput >= .8f)
        //{
        //    if (!_isGrounded) return;
        //    _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //}

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PowerUp")
        {
            collision.gameObject.SetActive(false);
            speed = 10f;
            jumpForce = 12f;
            StartCoroutine(ResetPower());
        }
    }

    void FixedUpdate()
    {
        float horizontalVelocity = _movement.normalized.x * speed;
        _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
    }


    void LateUpdate()
    {
        _animator.SetBool("idle", _movement == Vector2.zero);
        _animator.SetBool("isGrounded", _isGrounded);
        _animator.SetFloat("verticalVelocity", _rigidbody.velocity.y);
    }

    public void Movement()
    {
        // Movement
        //float horizontalInput = Input.GetAxisRaw("Horizontal"); // Computer movement
        float horizontalInput = SimpleInput.GetAxisRaw("Horizontal");

        _movement = new Vector2(horizontalInput, 0f);

        // Flip character
        if (horizontalInput < 0f && _facingRight == true)
        {
            Flip();
        }
        else if (horizontalInput > 0f && _facingRight == false)
        {
            Flip();
        }
    }

    public void Jump()
    {
        if(_isGrounded == true)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX *= -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(10);
        jumpForce = 7;
        speed = 5;
    }

    void OnDestroy()
    {
        obj = null;    
    }
}
