using System;
using UnityEngine;
using UnityEngine.AI;

namespace C__scripts
{
    public class Enemy : MonoBehaviour
    {
        public Transform spawnPosition;
        public float radius;
        public float speed;
        public Animator animator;
        
        private float position;
        private bool isMoveRight = true;
        private static readonly int Go = Animator.StringToHash("go");

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            position = transform.position.x;
            var vector = Vector2.right * (speed * Time.deltaTime);
            
            if (position + vector.x < spawnPosition.position.x + radius && isMoveRight)
            {
                transform.Translate(vector);
            }
            else if (position - vector.x > spawnPosition.position.x - radius)
            {
                isMoveRight = false;
                transform.eulerAngles = new Vector3(0, -180, 0);
                transform.Translate(vector);
            }
            else
            {
                isMoveRight = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            
            animator.SetTrigger(Go);
        }
    }
}
