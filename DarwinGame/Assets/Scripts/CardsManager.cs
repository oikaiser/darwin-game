using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardsManager : MonoBehaviour
{
    public GameObject levelCleared;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AllCardsCollected();
    }

    void AllCardsCollected() 
    {
        if (transform.childCount == 0)
        {
            levelCleared.gameObject.SetActive(true);
            Invoke("ChangeScene", 4);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
