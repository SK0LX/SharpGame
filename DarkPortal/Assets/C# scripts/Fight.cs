using System;
using System.Collections;
using C__scripts.Enemies;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;


public class Fight : MonoBehaviour
{
    public Player player;
    private Health playerHealth;
    private Enemy enemy;
    private Entity enemyComponent;
    
    private bool facingRight;
    public bool isPlayerTurn;
    private bool buttonClick;
    public Canvas canvas;
    public bool critDamage;

    public void Init(GameObject player, Canvas canvas, GameObject enemy)
    {
        this.player = player.GetComponent<Player>();
        this.canvas = canvas;
        this.enemy = enemy.GetComponent<Enemy>();
        enemyComponent = enemy.GetComponent<Entity>();
        playerHealth = this.player.GetComponent<Health>();
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
        player.isPlayerTorn = isPlayerTurn;
    }

    private int ChooseRandomDamage(int downDamage, int upDamage) // машина по рандомизированному урона(крит 20%)
    {
        if (upDamage - downDamage < 0)
            throw new ArgumentException("Верхний предел урона не может быть ниже нижнего предела урона");
        var rnd = new Random();
        var damage = rnd.Next(downDamage,upDamage + 1);
        if (rnd.Next(0, 101) < 20)
        {
            damage += (upDamage-downDamage) / 2;
            critDamage = true;
        }

        return damage;
    }
    
    private IEnumerator CoreFight() //сам весь процесс файта (очереди)
    {
        if (isPlayerTurn)
        {
            if (!buttonClick && (Input.GetKeyDown(KeyCode.Z) || player.activateButtonForAttack))
            {
                buttonClick = true;
                yield return StartCoroutine(player.Attack());
                enemyComponent.TakeDamage(ChooseRandomDamage(10, 15));
                if (enemyComponent.IsDead)
                {
                    yield return StartCoroutine(enemy.Die());
                    yield return new WaitForSeconds(1f);
                    player.EndFight();
                    canvas.enabled = false;
                    Destroy(gameObject);
                }
                isPlayerTurn = false;
                player.isPlayerTorn = false;
                buttonClick = false;
                player.activateButtonForAttack = false;
            }
            
            if (!buttonClick && (Input.GetKeyDown(KeyCode.X) || player.activateButtonForHealth))
            {
                buttonClick = true;
                yield return StartCoroutine(player.HealingPlayer());
                isPlayerTurn = false;
                player.isPlayerTorn = false;
                buttonClick = false;
                player.activateButtonForHealth = false;
            }
        }   
        else
        {
            if (!buttonClick)
            {
                buttonClick = true;
                yield return StartCoroutine(enemy.Attack());
                var damage = ChooseRandomDamage(enemyComponent.power - 2, enemyComponent.power + 2);
                playerHealth.TakeHit(damage * ChooseDamageSkip(player.inventory.dexterity)); 
                isPlayerTurn = true;
                player.isPlayerTorn = true;
                buttonClick = false;
            }
        } 
    }
    
    private int ChooseDamageSkip(int dexterity) // поможет ли ловкость уйти от урона, хммм
    {
        var rnd = new Random();
        return rnd.Next(0, 100) <= dexterity ? 0 : 1;
    }
}
    