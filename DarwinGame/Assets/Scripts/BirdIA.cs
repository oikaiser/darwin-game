using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdIA : MonoBehaviour
{
    private bool _movingRight = true;
    private float _speed = 2f;
    private int _damage = 1;
    private int _score = 50;
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
        transform.Translate(Vector2.right * _speed * Time.deltaTime);

        if (_movingRight == true)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Dañar a jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                PlayerHealth.obj.AddDamage(_damage);
            } else
            {
                PlayerHealth.obj.AddDamage(_damage);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Turn"))
        {
            if (_movingRight)
            {
                _movingRight = false;
            } else {
                _movingRight = true;
            }
        }

        // Destruir enemigo
        if (collision.gameObject.CompareTag("Player"))
        {
            getKilled();
        }
    }

    void getKilled()
    {
        StartCoroutine("VisualFeedback", 2f);
        GameManager.obj.AddScore(_score);
        UIManager.obj.UpdateScore();
    }

    IEnumerator VisualFeedback(float waitTime)
    {
        if (_rigidbody != null)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            _animator.SetTrigger("isDead");
            transform.GetChild(0).gameObject.SetActive(false);
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _rigidbody.velocity = Vector2.zero;
            _speed = 0f;
            yield return new WaitForSeconds(waitTime);
            gameObject.SetActive(false);
        }
    }
}
