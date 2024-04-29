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

    public void TakeHit(int damage)
    {
        health -= damage;
        hpBar.text = $"{health}/{maxHealth}";
        gameObject.GetComponent<HealthBar>().fill = (float)health / maxHealth;
        
        
        
        if (health <= 0)
        {
            StartCoroutine(Death());
            health = 0;
        }
        
    }

    public void SetHealth(int bonusHealth)
    {
        hpBar.text = $"{health}/{maxHealth}";
        health += bonusHealth;
        if (maxHealth < health)
        {
            health = maxHealth;
        }
    }


    IEnumerator Death()
    {
        Animator.SetTrigger("death");
        yield return new WaitForSeconds(1f);
    }
}
