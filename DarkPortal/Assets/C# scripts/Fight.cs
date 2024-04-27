using System.Collections;
using System.Collections.Generic;
using C__scripts;
using UnityEngine;
using System;
using Random = System.Random;


enum EntityForFight
{
    Mob = 1,
    Player = 2
}
public class Fight : MonoBehaviour
{
    public GameObject player;
    private bool facingRight;
    
    private bool isPlayerTurn = true;
    
    private Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = player.GetComponent<Animator>();
        RandomFight();
    }

    // Update is called once per frame
    void Update()
    { 
        if (TriggetTest.fight)
            StartCoroutine(RandomMashine());
        
    }


    private void RandomFight()
    {
        var rnd = new Random();
        isPlayerTurn = rnd.Next(1, 3) == 1;
    }


    private IEnumerator RandomMashine()
    {
        if (isPlayerTurn)
        {
            Debug.Log("Ход игрока");
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine(Attack());
                
                yield return StartCoroutine(Attack());
                
                isPlayerTurn = !isPlayerTurn;
            }
        }   
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                // Атака Моба
                isPlayerTurn = !isPlayerTurn;
            }
            Debug.Log("Ход противника");
        } 
    }
    
    
    IEnumerator Attack()
    {
        var geolocationNow = player.transform.position.x;
        var moveSpeed = 7f; // Скорость движения
        
        animator.SetTrigger("runForAttack1");
        while (player.transform.position.x < geolocationNow + 10)
        {
            player.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }

        animator.SetTrigger("attack1"); 
        yield return new WaitForSeconds(1f);
        
        Flip();
        
        animator.SetTrigger("runForAttack1");
        
        while (player.transform.position.x > geolocationNow)
        {
            player.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }
        
        animator.SetTrigger("default");
    }
    
    
    
    public void OnButtonClick()
    {
        StartCoroutine(Attack());
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().speed = 0;
        }
    }

    void Flip()
    {
        var spriteRenderer = player.GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
    