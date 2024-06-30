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
    public TextMeshProUGUI hpBarDefault; // review(30.06.2024): Разный нейминг у healthBar и hpBar, стоит остановиться на чем-то одном
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
        // review(30.06.2024): health = Math.Min(health + bonusHealth, maxHealth);
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
        // review(30.06.2024): Логика заполнения баров повторяется. Стоило выделить метод типа UpdateHealthView()
        hpBarDefault.text = $"{health}/{maxHealth}";
        healthBar.fill = (float)health / maxHealth;
    }
}
