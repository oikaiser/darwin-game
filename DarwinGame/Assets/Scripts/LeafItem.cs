using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafItem : MonoBehaviour
{
    [SerializeField] int score = 100;
	[SerializeField] GameObject lightingParticles;
	[SerializeField] GameObject burstParticles;
    private SpriteRenderer _rederer;
    private Collider2D _collider;

    void Awake()
	{
		_rederer = GetComponent<SpriteRenderer>();
		_collider = GetComponent<Collider2D>();
	}
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Add score
            GameManager.obj.AddScore(score);
            UIManager.obj.UpdateScore();

            // Disable collider
			_collider.enabled = false;

            // Visual stuff
			_rederer.enabled = false;
			lightingParticles.SetActive(false);
			burstParticles.SetActive(true);

            // Audio
            AudioManager.obj.PlayPickUp();

            // Destroy after some time
			Destroy(gameObject, 2f);
        }
    }
}
