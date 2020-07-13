using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeItem : MonoBehaviour
{
    [SerializeField] int health = 1;
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
		if (collision.CompareTag("Player") && PlayerHealth.obj.health < 3) 
        {

			// Cure pllayer
			collision.SendMessageUpwards("AddHealth", health);

			// Disable collider
			_collider.enabled = false;

			// Visual stuff
			_rederer.enabled = false;
			lightingParticles.SetActive(false);
			burstParticles.SetActive(true);

			// Destroy after some time
			Destroy(gameObject, 2f);
		}
	}
}
