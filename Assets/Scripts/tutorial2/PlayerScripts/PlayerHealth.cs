using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    

    private float health;
    private float lerpTimer;
    [Header("Health bar")]
    [SerializeField]private float maxHealth = 100;
    [SerializeField]private float chipSpeed = 2f;

    [SerializeField]private Image frontHealthBar;
    [SerializeField]private Image backHealthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    
    
    
    [Header("Damage Overlay")]
    [SerializeField] private Image overlay;
    [SerializeField] private float duration;
    [SerializeField] private float fadeSpeed;
    private float durationTimer;
    
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b, 0);
       
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health,0,maxHealth);
        UpdateHealthUI();
        if (overlay.color.a > 0)
        {
            if (health < 30)
                return;

            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;

                overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            
            percentComplete = MathF.Pow(percentComplete,2);
            backHealthBar.fillAmount = Mathf.Lerp(fillB,hFraction,percentComplete);

        } 
        
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;  
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;

            percentComplete = MathF.Pow(percentComplete,2);
            frontHealthBar.fillAmount = Mathf.Lerp(fillF,hFraction,percentComplete);

        }
        
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b, 1);
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}
