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
            StartCoroutine(CoreFight());
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
        }   
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                
                //TODO Катя, здесь жолжна быть логика моба, что он делает во время файта
                //я сделал корутину в корутине для того, чтобы мы ждали анимации, пока они выполнются
                //(так писали в инете + gpt)
                
                isPlayerTurn = !isPlayerTurn;
            }
            Debug.Log("Ход противника");
        } 
    }
    
    private void OnTriggerEnter2D(Collider2D other)//триггер
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Player>().speed = 0;
            player.GetComponent<Player>().fight = true;
            canvas.enabled = !canvas.enabled;
        }
    }
}
    