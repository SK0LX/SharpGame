using System;
using System.Collections;
using C__scripts.Enemies;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;


public class Fight : MonoBehaviour
{
    public Player player;
    private Health playerHealth;
    private Enemy enemy;
    
    private bool facingRight;
    private bool isPlayerTurn;
    private bool buttonClick;
    private Animator animator;
    public Canvas canvas;
    public bool critDamage;

    public void Init(GameObject player, Canvas canvas, GameObject enemy)
    {
        this.player = player.GetComponent<Player>();
        this.canvas = canvas;
        this.enemy = enemy.GetComponent<Enemy>();
        playerHealth = this.player.GetComponent<Health>();
        animator = player.GetComponent<Animator>();
        ChooseRandomMove();
    }
    
    void Update()
    {
        if (player.fight)
        {
            canvas.enabled = true;
            StartCoroutine(CoreFight());
        }
    }


    private void ChooseRandomMove() // типо монетку подбрасываем в начале файта
    {
        var rnd = new Random();
        isPlayerTurn = rnd.Next(1, 3) == 1;
        isPlayerTurn = false;
    }

    private int ChooseRandomDamage(int downDamage, int upDamage) // машина по рандомизированному урона(крит 20%)
    {
        if (upDamage - downDamage < 0)
            throw new ArgumentException("Верхний предел урона не может быть ниже нижнего предела урона");
        var rnd = new Random();
        var damage = rnd.Next(downDamage,upDamage + 1);
        if (rnd.Next(0, 101) < 20)
        {
            damage += (upDamage-damage) / 2;
            critDamage = true;
        }

        return damage;
    }
    
    private IEnumerator CoreFight() //сам весь процесс файта (очереди)
    {
        if (isPlayerTurn)
        {
            Debug.Log("Ход игрока");
            if (!buttonClick && Input.GetKeyDown(KeyCode.Z))
            {
                buttonClick = true;
                yield return StartCoroutine(player.Attack());
                isPlayerTurn = false;
                buttonClick = false;
            }
            
            if (!buttonClick && Input.GetKeyDown(KeyCode.X))
            {
                buttonClick = true;
                yield return StartCoroutine(player.HealingPlayer());
                isPlayerTurn = false;
                buttonClick = false;
            }
        }   
        else
        {
            Debug.Log("Ход противника");
            if (!buttonClick)
            {
                buttonClick = true;
                yield return StartCoroutine(enemy.Attack());
                playerHealth.TakeHit(10); 
                isPlayerTurn = true;
                buttonClick = false;
            }
        } 
    }
    
    IEnumerator ProtectionFromAttack()
    {
        if (GetComponent<Fight>().critDamage)
        {
            player.Animator.SetTrigger("CritDamage");
        }
        player.Animator.SetTrigger("Defend");
        yield return 1f;
        player.Animator.SetTrigger("default");
    }
}
    