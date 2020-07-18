using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager obj;
    public bool gamePaused;
    public int score = 0;
    public GameObject gameOverMenuUI;

    void Awake()
    {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() {
        gameOverMenuUI.SetActive(true);
        PlayerController.obj.enabled = false;
        PlayerController.obj.GetComponent<Collider2D>().enabled = false;
    }

    public void AddScore(int scoreGive)
    {
        Debug.Log("Score give " + scoreGive);
        score += scoreGive;
    }

    void OnDestroy()
    {
        obj = null;  
    }
}
