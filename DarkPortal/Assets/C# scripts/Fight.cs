using System;
using System.Collections;
using C__scripts.Enemies;
using UnityEngine;
using Random = System.Random;


public class Fight : MonoBehaviour
{
    public Player player;
    private Health playerHealth;
    private Enemy enemy;
    private Entity enemyComponent;
    
    private bool facingRight;
    public bool isPlayerTurn; // review(24.05.2024): Это поле дублирует поле игрока
    private bool isBossFight;
    private bool buttonClick;
    public Canvas canvas;
    public bool critDamage;

    private bool inDialogue;
    private bool goFight;
    
    public void Init(GameObject player, Canvas canvas, GameObject enemy)
    {
        this.player = player.GetComponent<Player>();
        this.canvas = canvas;
        this.enemy = enemy.GetComponent<Enemy>();
        isBossFight = enemy.GetComponent<Boss>() is not null;
        enemyComponent = enemy.GetComponent<Entity>();
        playerHealth = this.player.GetComponent<Health>();
        ChooseRandomMove();
        goFight = false;
        inDialogue = false;
    }

    void Update()
    {
        if (!inDialogue)
        {
            inDialogue = true;
            player.dialogForMobs.StartMessage();
        }

// После завершения диалога проверяем, можно ли начинать бой
        if (inDialogue && (player.dialogForMobs.EndDialog() || goFight))
        {
            goFight = true;
            canvas.enabled = true;
            if (player.sr.flipX)
                player.sr.flipX = false;
            StartCoroutine(CoreFight());
        }
    }

    // review(24.05.2024): Choose first step fighter 
    private void ChooseRandomMove() // типо монетку подбрасываем в начале файта
    {
        var rnd = new Random();
        isPlayerTurn = rnd.Next(1, 3) == 1;
        player.isPlayerTorn = isPlayerTurn;
    }

    private int ChooseRandomDamage(int downDamage, int upDamage) // машина по рандомизированному урона(крит 20%)
    {
        critDamage = false;
        if (upDamage - downDamage < 0)
            throw new ArgumentException("Верхний предел урона не может быть ниже нижнего предела урона");
        var rnd = new Random(); // review(24.05.2024): Может, создать один Random и поместить его в поле?
        var damage = rnd.Next(downDamage,upDamage + 1);
        if (rnd.Next(0, 101) < 20) // review(24.05.2024): Как будто не хватает exntension-метода rnd.FlipCoin(int percent)
        {
            Debug.Log(!isPlayerTurn ? $"CRIT mob={enemy.name}" : $"CRIT player");
            damage += (upDamage-downDamage) / 2;
            critDamage = true;
        }

        return damage;
    }
    
    private IEnumerator CoreFight() //сам весь процесс файта (очереди)
    {
        // review(24.05.2024): Было бы круто выделить методы RunPlayerStep/RunEnemyStep
        if (isPlayerTurn)
        {
            // review(24.05.2024): Долго пытался понять, что такое buttonClick. Потом осознал, что это clickGuard (типа того). Стоит переименовать, чтобы было понятно.
            // А еще лучше, наверное, вынести эту проверку вне if-ов, чтобы было так:
            // if (!clickGuard)
            // {
            //    clickGuard = true;
            //    // logic
            //    clickGuard = false;
            // }
            // Но мне кажется, что есть менее костыльные способы реализовать эту логику
            if (!buttonClick && (Input.GetKeyDown(KeyCode.Z) || player.activateButtonForAttack))
            {
                // review(24.05.2024): Стоит выделить методы PlayPlayerAttackStep/PlayPlayerHealingStep
                buttonClick = true;
                yield return StartCoroutine(player.Attack());
                enemyComponent.TakeDamage(ChooseRandomDamage(10, 15));
                if (enemyComponent.IsDead)
                {
                    yield return StartCoroutine(enemy.Die());
                    yield return new WaitForSeconds(1f);
                    Debug.Log($"{isBossFight}");
                    if (!isBossFight)
                        player.WinCanvas();
                    player.inventory.coins += enemy.cost;
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
                var damage = ChooseRandomDamage(enemyComponent.power - 2, enemyComponent.power + 2);
                yield return StartCoroutine(enemy.Attack());
                playerHealth.TakeHit(damage * ChooseDamageSkip(player.inventory.dexterity)); 
                isPlayerTurn = true;
                player.isPlayerTorn = true;
                buttonClick = false;
            }
        }
    }

    // review(24.05.2024): Почему не bool?
    private int ChooseDamageSkip(int dexterity) // поможет ли ловкость уйти от урона, хммм
    {
        var rnd = new Random();
        return rnd.Next(0, 100) <= dexterity ? 0 : 1;
    }
}
    