using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Entity _player;
    public float speed = 1f;
    private Rigidbody2D rb;
    public float jumpForce;
    private SpriteRenderer sr;

    public PlayerController()
    {
        _player = new Entity(10, 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        sr.flipX = movement < 0;
    }
}
