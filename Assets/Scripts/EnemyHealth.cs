using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public float startingHealth;
    public ParticleSystem deathParticle;

    private float currentHealth;
    private Slider healthBar;

	// Use this for initialization
	void Awake () {
        healthBar = GetComponentInChildren<Slider>();
        currentHealth = startingHealth;
        healthBar.maxValue = startingHealth;
	}

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UIUpdate();
        if (currentHealth <= 0)
            Death(); 
    }

    public void UIUpdate()
    {
        healthBar.value = currentHealth;
    }

    public void Death()
    {
        Destroy(gameObject);
        ParticleSystem particle= Instantiate(deathParticle, transform.position, transform.rotation);
        particle.gameObject.hideFlags= HideFlags.HideInHierarchy;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
