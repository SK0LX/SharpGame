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
    
    public Animator Animator;
    public TextMeshProUGUI hpBarDefault;
    [SerializeField] private AudioSource hit;
    [SerializeField] private AudioSource deathYourHeroIsDead;
    [SerializeField] private AudioSource screamDeath;

    public void TakeHit(int damage)
    {
        health -= damage; // review(24.05.2024): Сюда стоит вставить Math.Max(0, health - damage);
        hit.Play();
        // review(24.05.2024): Компонент HealthBar стоит захватить в void Awake() и поместить в поле
        gameObject.GetComponent<HealthBar>().fill = (float)health / maxHealth;
        
        if (health <= 0)
        {
            StartCoroutine(Death());
            health = 0;
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
        gameObject.GetComponent<HealthBar>().fill = (float)health / maxHealth;
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
        gameObject.GetComponent<HealthBar>().fill = (float)health / maxHealth;
    }
}
