using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D rb;
    public float jumpForce;
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
        if (Input.GetKey(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        sr.flipX = moveInput < 0;
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
}
