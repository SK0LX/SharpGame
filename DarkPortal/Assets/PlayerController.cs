using System.Collections;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Animator Animator;
    private float moveInput;
    private int hp;
    private int dexterity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
        
    void FixedUpdate()
    {
        if (TriggetTest.fight)
            CallEvent(); // в основном - проверка, какие кнопки мы нажали
        
        StartCoroutine(movementPlayer()); // хождение чубрика
    }

    private void CallEvent()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            var health = gameObject.GetComponent<Health>();
            health.TakeHit(10);
        }
    }
    
    
    private IEnumerator movementPlayer()
    {
        var horizontalMove = Input.GetAxis("Horizontal") * speed;
        if (!TriggetTest.fight)
        {
            moveInput = Input.GetAxis("Horizontal");
            transform.position += new Vector3(moveInput, 0, 0) * speed * Time.deltaTime;
            Animator.SetFloat("HorizontalMove", Mathf.Abs(horizontalMove));

            if (moveInput < 0)
            {
                sr.flipX = true;
            }

            if (moveInput > 0)
            {
                sr.flipX = false;
            }
        }
        else
        {
            Animator.SetFloat("HorizontalMove", 0);
            yield return new WaitForSeconds(1f);
        }
    }
}
