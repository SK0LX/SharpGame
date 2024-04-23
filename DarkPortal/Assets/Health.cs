using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Animator Animator;
    public Text Bar;

    public void TakeHit(int damage)
    {
        health -= damage;

        gameObject.GetComponent<HealthBar>().fill = (float)health / maxHealth;
        
        if (health < 0)
        {
            StartCoroutine(Death());
            
        }
        
    }

    public void SetHealth(int bonusHealth)
    {
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
