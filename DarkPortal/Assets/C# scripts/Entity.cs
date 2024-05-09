using System;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Random = System.Random;

public class Entity: MonoBehaviour
{
    [SerializeField] public int maxHealth;
    [SerializeField] public int dexterity;
    [SerializeField] public int power;
    [SerializeField] private Canvas prefabHpCanvas;
    
    private Canvas canvasHp;
    private Image bar1;
    private TextMeshProUGUI text;
    public int health;
    private Bounds colliderForm;
    private RectTransform positionCanvas;
    // public List<int> InventoryForWeapons;
    private Skills skill;
    public List<PlayerInventory> InventoryList; 

    public void Start()
    {
        InitCanvas();
        health = maxHealth;
        // InventoryForWeapons = new List<int>();
        skill = gameObject.GetComponent<Skills>();
        InventoryList = new List<PlayerInventory>();
    }

    public void Update()
    {
        if (!IsDead && canvasHp.enabled)
            positionCanvas.position = new Vector3(transform.position.x, positionCanvas.position.y, 0);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateCanvasHp();
    }

    public bool IsDead => health <= 0;

    
    public void UseSkills()
    {
        switch (new Random().Next(0, 3))
        {
            case 0:
                skill.BuffHeal(this, maxHealth / 6);
                text.text = $"{health}/{maxHealth}";
                break;
            case 1:
                skill.BuffDexterity(this, dexterity / 5);
                break;
            case 2:
                skill.BuffPower(this, 1);
                break;
        }
    }

    public void AddToInventory(PlayerInventory item)
    {
        InventoryList.Add(item);
    }

    public void RemoveFromInventory(PlayerInventory item)
    {
        InventoryList.Remove(item);
    }

    public void ShowCanvas()
    {
        canvasHp.enabled = true;
    }
    
    private void UpdateCanvasHp()
    {
        bar1.fillAmount = (float)health / maxHealth;
        text.text = $"{health}/{maxHealth}";
        if (health <= 0)
            Destroy(canvasHp.gameObject);
    }

    private void InitCanvas()
    {
        canvasHp = Instantiate(prefabHpCanvas);
        canvasHp.enabled = false;
        positionCanvas = canvasHp.GetComponent<RectTransform>();
        colliderForm = gameObject.GetComponent<Collider2D>().bounds;
        positionCanvas.position = colliderForm.max;
        
        positionCanvas.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, colliderForm.max.x);
        bar1 = canvasHp.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        text = canvasHp.gameObject.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        text.text = $"{health}/{maxHealth}";
    }
}
