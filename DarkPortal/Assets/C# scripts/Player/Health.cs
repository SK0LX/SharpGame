using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public HealthBar healthBar;
    
    public Animator Animator;
    public TextMeshProUGUI hpBarDefault;
    [SerializeField] private AudioSource hit;
    [SerializeField] private AudioSource deathYourHeroIsDead;
    [SerializeField] private AudioSource screamDeath;
    

    private void Awake()
    {
        health = DataHolder.health;
        maxHealth = DataHolder.maxHealth;
        hpBarDefault.text = $"{health}/{maxHealth}";
        healthBar.fill = (float)health / maxHealth;
    }

    public void TakeHitSound()
    {
        hit.Play();
    }

    public void TakeHit(int damage)
    {
        health = Math.Max(0, health - damage);;
        healthBar.fill = (float)health / maxHealth;
        
        if (health == 0)
        {
            StartCoroutine(Death());
        }
        hpBarDefault.text = $"{health}/{maxHealth}";
    }

    public void SetHealth(int bonusHealth)
    {
        health += bonusHealth;
        if (maxHealth < health)
        {
            health = maxHealth;
        }
        hpBarDefault.text = $"{health}/{maxHealth}";
        healthBar.fill = (float)health / maxHealth;
    }

    IEnumerator Death()
    {
        Animator.SetTrigger("death");
        yield return new WaitForSeconds(0.2f);
        screamDeath.Play();
        yield return new WaitForSeconds(1f);
        deathYourHeroIsDead.Play();
    }
    
    public void DecorationBoost(int bonusHealth)
    {
        health += bonusHealth;
        maxHealth += bonusHealth;
        hpBarDefault.text = $"{health}/{maxHealth}";
        healthBar.fill = (float)health / maxHealth;
    }
}
