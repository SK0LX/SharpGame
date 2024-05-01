using System;
using System.Collections.Generic;
using UnityEngine;

public class Entity: MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int dexterity;
    [SerializeField] public int power;
    
    private GameObject obj;
    // public List<int> InventoryForWeapons;
    public List<Skills> SkillsList;
    public List<PlayerInventory> InventoryList; 

    public void Start()
    {
        // InventoryForWeapons = new List<int>();
        SkillsList = new List<Skills>(); // через .GetComponent
        InventoryList = new List<PlayerInventory>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public bool IsDead()
    {
        return health <= 0;
    }
    
    public void UseSkills(Skills skill, Entity entity)
    {
        skill.BuffHeal(entity, 20); // 
        skill.BuffDexterity(entity, 10);
        skill.BuffPower(entity, 10);
        skill.SomeDamage(entity, entity.power);
        skill.AllDamage(entity);
    }

    public void AddToInventory(PlayerInventory item)
    {
        InventoryList.Add(item);
    }

    public void RemoveFromInventory(PlayerInventory item)
    {
        InventoryList.Remove(item);
    }
}
