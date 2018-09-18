using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

   // public delegate void deathAction();
  //  public static event deathAction onDeath;

    public ParticleSystem deathParticle;

    public EnemyData enemyData;

    private float currentHealth;
    private Slider healthBar;

	// Use this for initialization
	void Awake () {
        healthBar = GetComponentInChildren<Slider>();
        currentHealth = enemyData.health;
        healthBar.maxValue = enemyData.health;
        UIUpdate();
	}

    private void OnEnable()
    {
        //onDeath += Death;
    }

    private void OnDisable()
    {
       // onDeath -= Death;
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

    private void Death()
    {
        Destroy(gameObject);
        ParticleSystem particle= Instantiate(deathParticle, transform.position, transform.rotation);
        particle.gameObject.hideFlags= HideFlags.HideInHierarchy;
        GameManager.instance.addFood(enemyData.foodBounty);
        GameManager.instance.addMoney(enemyData.moneyBounty);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
