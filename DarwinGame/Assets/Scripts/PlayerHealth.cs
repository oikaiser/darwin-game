using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth obj;
    [SerializeField] private int totalHealth = 3;
    public int health;
    private float heartSize = 16f;

    void Awake() 
    {
        obj = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;
    }

    public void AddHealth(int amount)
    {
        health += amount;

        // Max health
        if (health > totalHealth)
        {
            health = totalHealth;
        }

        Debug.Log("Player got some life: His current health is " + health);
    }

    // Update is called once per frame
    void Update()
    {
        
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
