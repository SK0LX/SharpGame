using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Searcher;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Animator Animator;
    private bool facingRight = true;
    private float moveInput;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate() // тут в основном как ходит чубрик
    {
        moveInput = Input.GetAxis("Horizontal");
        transform.position += new Vector3(moveInput, 0, 0) * speed * Time.deltaTime;
        var horizontalMove = Input.GetAxis("Horizontal") * speed;
        Animator.SetFloat("HorizontalMove", Mathf.Abs(horizontalMove));
        switch (facingRight)
        {
            case false when moveInput > 0:
            case true when moveInput < 0:
                Flip();
                break;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Attack());
        }
        sr.flipX = moveInput < 0;
    }
    
    public void OnButtonClick()
    {
        StartCoroutine(Attack());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fight"))
        {
            speed = 0;
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        var Scalar = transform.localScale;
        Scalar.x *= -1;
    }
    
    void FlipForFight()
    {
        facingRight = !facingRight;
        var Scalar = transform.localScale;
        Scalar.x *= -1;
        transform.localScale = Scalar;
    }

    IEnumerator Attack()
    {
        var geolocationNow = transform.position.x;
        var moveSpeed = 50f; // Скорость движения
        
        Animator.SetTrigger("run");
        while (transform.position.x < geolocationNow + 70)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }

        Animator.SetTrigger("attack");
        yield return new WaitForSeconds(1f);
        FlipForFight();
        
        Animator.SetTrigger("run");
        while (transform.position.x > geolocationNow)
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }
        
        FlipForFight();
        Animator.SetTrigger("default");
    }
}
