using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 1;
    public float speed = 3f;
    public Transform groundDectection;
    public int score = 50;
    private bool movingRight = true;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

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
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Evitar caer en precipicio
        RaycastHit2D _isGroundFloor = Physics2D.Raycast(groundDectection.position, Vector2.down, 2.0f, LayerMask.GetMask("Ground"));
        
        if (_isGroundFloor.collider == false)
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                movingRight = false;
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D point in collision.contacts)
        {
            
        }
        // Dañar a jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                PlayerHealth.obj.AddDamage(damage);
            } else
            {
                PlayerHealth.obj.AddDamage(damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // Destruir enemigo
        if (collision.gameObject.CompareTag("Player"))
        {
            getKilled();
        }

    }

    private void getKilled()
    {
        StartCoroutine("VisualFeedback", 2f);
        GameManager.obj.AddScore(score);
        UIManager.obj.UpdateScore();
    }

    private IEnumerator VisualFeedback(float waitTime)
    {
        if (_rigidbody != null)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            _animator.SetTrigger("isDead");
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _rigidbody.velocity = Vector2.zero;
            speed = 0f;
            yield return new WaitForSeconds(waitTime);
            gameObject.SetActive(false);
        }
    }
}
