using UnityEngine;

public class BossEvents : MonoBehaviour
{
    private BoxCollider2D[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponents<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckCollider(int current)
    {
        if (current != 1)
            colliders[current-1].enabled = false;
        colliders[current].enabled = true;
    }
}
