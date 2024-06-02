using System;
using System.Collections;
using C__scripts.Enemies;
using TMPro;
using UnityEngine;
using Random = System.Random;


public class Fight : MonoBehaviour
{
    public Player player;
    private Health playerHealth;
    public Enemy enemy;
    private Entity enemyComponent;
    
    private bool facingRight;
    private bool isBossFight;
    private bool clickGuardButtonClick;
    public Canvas canvas;
    public bool critDamage;

    private bool inDialogue;
    private bool goFight;
    private Random rnd = new ();
    private TextMeshProUGUI critText;
    
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
        critText = canvas.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!inDialogue && !enemy.isFullTimeSpawn)
        {
            inDialogue = true;
            player.dialogForMobs.StartMessage();
        }

// После завершения диалога проверяем, можно ли начинать бой
        if (inDialogue && (player.dialogForMobs.EndDialog() || goFight) || enemy.isFullTimeSpawn)
        {
            goFight = true;
            canvas.enabled = true;
            if (player.sr.flipX)
                player.sr.flipX = false;
            StartCoroutine(CoreFight());
        }
    }


    private void ChooseRandomMove() // типо монетку подбрасываем в начале файта
    {
        player.isPlayerTorn = rnd.Next(1, 3) == 1;
    }

    private int ChooseRandomDamage(int downDamage, int upDamage) // машина по рандомизированному урона(крит 20%)
    {
        critDamage = false;
        if (upDamage - downDamage < 0)
            throw new ArgumentException("Верхний предел урона не может быть ниже нижнего предела урона");
        var damage = rnd.Next(downDamage,upDamage + 1);
        if (rnd.Next(0, 101) < 20)
        {
            critText.enabled = true;
            Debug.Log(!player.isPlayerTorn ? $"CRIT mob={enemy.name}" : $"CRIT player");
            damage += (upDamage-downDamage) / 2;
            critDamage = true;
        }

        return damage;
    }
    
    private IEnumerator CoreFight() //сам весь процесс файта (очереди)
    {
        if (player.isPlayerTorn)
        {
            if (!clickGuardButtonClick && (Input.GetKeyDown(KeyCode.Z) || player.activateButtonForAttack))
            {
                clickGuardButtonClick = true;
                var damage = ChooseRandomDamage(10, 15);
                yield return StartCoroutine(player.Attack());
                critText.enabled = false;
                enemyComponent.TakeDamage(damage);
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
                player.isPlayerTorn = false;
                player.isPlayerTorn = false;
                clickGuardButtonClick = false;
                player.activateButtonForAttack = false;
            }
            
            if (!clickGuardButtonClick && (Input.GetKeyDown(KeyCode.X) || player.activateButtonForHealth))
            {
                clickGuardButtonClick = true;
                yield return StartCoroutine(player.HealingPlayer());
                player.isPlayerTorn = false;
                player.isPlayerTorn = false;
                clickGuardButtonClick = false;
                player.activateButtonForHealth = false;
            }
        }   
        else
        {
            if (!clickGuardButtonClick)
            {
                clickGuardButtonClick = true;
                var damage = ChooseRandomDamage(enemyComponent.power - 2, enemyComponent.power + 2);
                yield return StartCoroutine(enemy.Attack());
                critText.enabled = false;
                playerHealth.TakeHit(damage * ChooseDamageSkip(player.inventory.dexterity)); 
                player.isPlayerTorn = true;
                player.isPlayerTorn = true;
                clickGuardButtonClick = false;
            }
        }
    }
    
    private int ChooseDamageSkip(int dexterity) // поможет ли ловкость уйти от урона, хммм
    {
        var rnd = new Random();
        return rnd.Next(0, 100) <= dexterity ? 0 : 1;
    }
}
    