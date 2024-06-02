using System.Collections;
using UnityEngine;

public class ADtoMove : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    private bool isButtonClicked;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveInput = Input.GetAxis("Horizontal");
        
        if (moveInput != 0f)
        {
            if (!isButtonClicked)
            {
                isButtonClicked = true;
                StartCoroutine(Disappear());
            }
        }
    }

    private IEnumerator Disappear()
    {
        for (var i = 200/255f; i > -0.05f; i -= 0.05f)
        {
            var color = spriteRenderer.color;
            color.a = i;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }
}
