using System;
using System.Collections;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Animator Animator;
    public Health HP;
    private bool facingRight = true;
    private float moveInput;
    private int hp;
    private int dexterity;
    public bool fight;
    public Canvas canvasDefault;
    public PlayerInventory inventory;
    public Button buttonForAttack;
    public bool activateButtonForAttack;
    public Button buttonForHealth;
    public bool activateButtonForHealth;
    public bool isPlayerTorn;

    public Button win;
    public Canvas canvasWin;
    [SerializeField] private AudioSource step;
    [SerializeField] private AudioSource damageMob;
    [SerializeField] private AudioSource heal;

    public Canvas canvasForDead;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        buttonForAttack.GetComponent<Button>().onClick.AddListener(ButtonAttack);
        buttonForHealth.GetComponent<Button>().onClick.AddListener(ButtonHealth);
        win.GetComponent<Button>().onClick.AddListener(EndFight);
        canvasDefault.enabled = true;
        canvasForDead.enabled = false;
        canvasWin.enabled = false;
    }
        
    void FixedUpdate()
    {
        movementPlayer();
        step.Play();
        if (HP.health == 0)
        {
            StartCoroutine(Dead());
        }
    }
    

    private void movementPlayer() // хождение чубрика на AD
    {
        var horizontalMove = Input.GetAxis("Horizontal") * speed;
        if (!fight)
        {
            moveInput = Input.GetAxis("Horizontal");
            transform.position += new Vector3(moveInput, 0, 0) * (speed * Time.deltaTime);
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
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
        var moveSpeed = 3f; // Скорость движения
        
        Animator.SetTrigger("runForAttack1");
        while (transform.position.x < geolocationNow + 1)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }

        Animator.SetTrigger("attack1");
        yield return new WaitForSeconds(1f);
        FlipForFight();
        damageMob.Play();
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
        gameObject.GetComponent<Health>().SetHealth(ChooseRandomHealth(3, 7));
        yield return new WaitForSeconds(1f);
        Animator.SetTrigger("default");
        heal.Play();
    }
    
    private int ChooseRandomHealth(int downHealth, int upHealth) // машина по рандомизированному хп(крит 20%)
    {
        if (upHealth - downHealth < 0)
            throw new ArgumentException("Верхний предел урона не может быть ниже нижнего предела урона");
        var rnd = new Random();
        var health = rnd.Next(downHealth,upHealth + 1);
        if (rnd.Next(0, 101) < 20)
        {
            health += (upHealth-downHealth) / 2;
        }

        return health;
    }
    
    private void EndFight()
    {
        canvasWin.enabled = false;
        fight = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        speed = 5f;
    }
    
    private void ButtonAttack()
    {
        if (isPlayerTorn)
            activateButtonForAttack = true;
    }
    
    private void ButtonHealth()
    {
        if (isPlayerTorn)
            activateButtonForHealth = true;
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(3f);
        canvasForDead.enabled = true;
    }
    
}
