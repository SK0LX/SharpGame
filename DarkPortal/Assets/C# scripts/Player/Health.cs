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
    public TextMeshProUGUI hpBar;
    public TextMeshProUGUI hpBarDefault;

    public void TakeHit(int damage)
    {
        health -= damage;
        gameObject.GetComponent<HealthBar>().fill = (float)health / maxHealth;
        
        if (health <= 0)
        {
            StartCoroutine(Death());
            health = 0;
        }
        hpBar.text = $"{health}/{maxHealth}";
        hpBarDefault.text = $"{health}/{maxHealth}";
    }

    public void SetHealth(int bonusHealth)
    {
        health += bonusHealth;
        if (maxHealth < health)
        {
            health = maxHealth;
        }
        hpBar.text = $"{health}/{maxHealth}";
        hpBarDefault.text = $"{health}/{maxHealth}";
        gameObject.GetComponent<HealthBar>().fill = (float)health / maxHealth;
    }


    IEnumerator Death()
    {
        Animator.SetTrigger("death");
        yield return new WaitForSeconds(1f);
    }
}
