using System.Collections;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Animator Animator;
    public Health HP;
    private bool facingRight = true;
    private float moveInput;
    private int hp;
    private int dexterity;
    public bool fight;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
        
    void FixedUpdate()
    {
        movementPlayer(); 
    }
    

    private void movementPlayer() // хождение чубрика на AD
    {
        var horizontalMove = Input.GetAxis("Horizontal") * speed;
        if (!fight)
        {
            moveInput = Input.GetAxis("Horizontal");
            transform.position += new Vector3(moveInput, 0, 0) * speed * Time.deltaTime;
            Animator.SetFloat("HorizontalMove", Mathf.Abs(horizontalMove));

            switch (moveInput)
            {
                case < 0:
                    sr.flipX = true;
                    break;
                case > 0:
                    sr.flipX = false;
                    break;
            }
        }
        else
        {
            Animator.SetFloat("HorizontalMove", 0);
        }
    }

    public void OnButtonClick() //кнопка на интерфейсе(TODO настроить)
    {
        StartCoroutine(Attack());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            speed = 0;
        }
    }
    
    void FlipForFight()
    {
        facingRight = !facingRight;
        var Scalar = transform.localScale;
        Scalar.x *= -1;
        transform.localScale = Scalar;
    }

    public IEnumerator Attack()
    {
        var geolocationNow = transform.position.x;
        var moveSpeed = 7f; // Скорость движения
        
        Animator.SetTrigger("runForAttack1");
        while (transform.position.x < geolocationNow + 10)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }

        Animator.SetTrigger("attack1");
        yield return new WaitForSeconds(1f);
        FlipForFight();
        
        Animator.SetTrigger("runForAttack1");
        
        while (transform.position.x > geolocationNow)
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }
        
        FlipForFight();
        Animator.SetTrigger("default");
    }

    public IEnumerator HealingPlayer()
    {
        Animator.SetTrigger("HealthPlayer");
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<Health>().SetHealth(5);
        yield return new WaitForSeconds(1f);
        Animator.SetTrigger("default");
    }
}
