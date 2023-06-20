using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer; 
    public float maxHealth = 100f;
    public float chipSpeed = 2f;

    public Image frontHealthBar;
    public Image backHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;    
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if(Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(Random.Range(5, 10)); 
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RestoreHealth(Random.Range(5, 10));
        }
    }

    public void UpdateHealthUI()
    {
        Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth; 
        if(fillB > hFraction)
        {
            //For front health bar
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;

            //For back health bar
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f; 
    }
}
