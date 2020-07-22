using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalCard : MonoBehaviour
{
    public GameObject card;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            card.SetActive(true);
            Time.timeScale = 0f;
            PlayerController.obj.enabled = false;
        }
    }
}
