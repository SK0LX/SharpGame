using System.Collections;
using UnityEngine;
using Random = System.Random;


public class Fight : MonoBehaviour
{
    public GameObject player;
    private bool facingRight;
    private bool isPlayerTurn = true;
    private bool buttonClick;
    private Animator animator;
    public Canvas canvas;

        
    void Start()
    {
        animator = player.GetComponent<Animator>();
        ChooseRandomMove();
    }
    
    void Update()
    {
        if (player.GetComponent<Player>().fight)
        {
            canvas.enabled = true;
            StartCoroutine(CoreFight());
        }
    }


    private void ChooseRandomMove() // типо монетку подбрасываем в начале файта
    {
        var rnd = new Random();
        isPlayerTurn = rnd.Next(1, 3) == 1;
    }


    private IEnumerator CoreFight() //сам весь процесс файта (очереди)
    {
        if (isPlayerTurn)
        {
            Debug.Log("Ход игрока");
            if (!buttonClick && Input.GetKeyDown(KeyCode.Z))
            {
                buttonClick = true;
                yield return StartCoroutine(player.GetComponent<Player>().Attack());
                isPlayerTurn = !isPlayerTurn;
                buttonClick = false;
            }
            
            if (!buttonClick && Input.GetKeyDown(KeyCode.X))
            {
                buttonClick = true;
                yield return StartCoroutine(player.GetComponent<Player>().HealingPlayer());
                isPlayerTurn = !isPlayerTurn;
                buttonClick = false;
            }
        }   
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                
                //TODO Катя, здесь жолжна быть логика моба, что он делает во время файта
                //я сделал корутину в корутине для того, чтобы мы ждали анимации, пока они выполнются
                //(так писали в инете + gpt)
                player.GetComponent<Health>().TakeHit(10); // это для удара
                isPlayerTurn = !isPlayerTurn;
            }
            Debug.Log("Ход противника");
        } 
    }
}
    