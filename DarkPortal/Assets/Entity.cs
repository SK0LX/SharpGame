using System;
using System.Collections.Generic;

public class Entity
{
    public int Health;

    public int Dexterity;

    public List<int> Skils; //для скилов нужно будет отдельно все сделать (класс Item)
    public List<int> InventoryForWeapons; // специально нужно будет сделать класс для оружия
    public List<int> Inventory; // инвентарь 

    public Entity(int health, int dexterity)
    {
        if (health < 0)
            throw new ArgumentException("Жизней не может быть меньше 0");
        this.Health = health;
        if (dexterity < 0)
            throw new ArgumentException("Ловкость не может быть меньше 0");
        this.Dexterity = dexterity;
        Skils = new List<int>();
        InventoryForWeapons = new List<int>();
        Inventory = new List<int>();
    }
}