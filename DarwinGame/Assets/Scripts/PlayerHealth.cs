using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth obj;
    [SerializeField] private int totalHealth = 3;
    [SerializeField] RectTransform healthUI;
    public int health;
    private float floraSize = 100f;
    
    [Header("Invulnerability")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody;

    void Awake() 
    {
        obj = this;
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;
    }

    public void AddDamage(int amount)
    {
        health -= amount;

        // Visual Feedback
        StartCoroutine("Hit");

        // Game Over
        if (health <= 0)
        {
            health = 0;
            GameManager.obj.GameOver();
        }

        healthUI.sizeDelta = new Vector2(floraSize * health, floraSize);

        Debug.Log("Player got damaged. His current health is " + health);
    }

    public void AddHealth(int amount)
    {
        health += amount;

        // Max health
        if (health > totalHealth)
        {
            health = totalHealth;
        }

        healthUI.sizeDelta = new Vector2(floraSize * health, floraSize);

        Debug.Log("Player got some life: His current health is " + health);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Hit()
    {
        if (_rigidbody != null)
        {
            _rigidbody.velocity = Vector2.zero;
            
            StartCoroutine("VisualFeedback");
            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator VisualFeedback()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while(temp < numberOfFlashes)
        {
            _renderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            _renderer.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }


    void OnEnable()
    {
        health = totalHealth;
    }

    void OnDestroy()
    {
        obj = null;
    }
}
