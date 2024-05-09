using UnityEngine;

public class Skills : MonoBehaviour
{
    void Damage(Entity entity, int damage)
    {
        entity.TakeDamage(damage);
    }

    void Heal(Entity entity, int quantity)
    {
        entity.health += quantity;
    }

    void Dexterity(Entity entity, int quantity)
    {
        entity.dexterity += quantity;
    }

    void Power(Entity entity, int quantity)
    {
        entity.power += quantity;
    }

    public void BuffHeal(Entity entity, int quantity)
    {
        entity.health += quantity;
        entity.maxHealth += quantity;
    }
    
    public void BuffDexterity(Entity entity, int quantity)
    {
        entity.dexterity += quantity;
    }
    
    public void BuffPower(Entity entity, int quantity)
    {
        entity.power += quantity;
    }
    
    public void AllDamage(Entity entity)
    {
        Destroy(entity.gameObject);
    }
    
    public void SomeDamage(Entity entity, int damage)
    {
        entity.health -= damage;
    }
}