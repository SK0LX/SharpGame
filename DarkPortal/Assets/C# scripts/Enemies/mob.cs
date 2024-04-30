using System;
using UnityEngine;

namespace C__scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private Animator animator;
        private new GameObject gameObject;
        private new Transform transform;
        private GameObject prefab;
        private Transform spawnPosition;
        private GameObject player;
        
        private float radius;
        private float speed;

        private float position;
        private bool isMoveRight = true;
        private static readonly int Go = Animator.StringToHash("go");
        private static readonly int AttackAnimation = Animator.StringToHash("attack");
        private static readonly int Idle = Animator.StringToHash("idle");

        public EnemyState State { get; private set; }

        public void Init(GameObject gameObject, float radius, float speed, Transform spawnPosition, GameObject player)
        {
            this.gameObject = gameObject;
            position = spawnPosition.position.x;
            animator = gameObject.GetComponent<Animator>();
            this.radius = radius;
            this.speed = speed;
            this.spawnPosition = spawnPosition;
            this.player = player;
            transform = gameObject.transform;
            State = EnemyState.Born;
        }
        
        public void EnemyTurn()
        {
            State = EnemyState.Attack;
        }

        public void PlayerTurn()
        {
            State = EnemyState.Wait;
        }
        
        public void Update()
        {
            if (gameObject is null) return;
            switch (State)
            {
                case EnemyState.None:
                    return;
                case EnemyState.Born:
                    State = EnemyState.Move;
                    return;
                case EnemyState.Move:
                    Move();
                    return;
                case EnemyState.PrepareToFight:
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    return;
                case EnemyState.Attack:
                    Attack();
                    return;
                case EnemyState.Wait:
                    return;
                case EnemyState.Die:
                    Die();
                    return;
            }
        }

        private void Move()
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

        private void Attack()
        {
            animator.SetTrigger(AttackAnimation);
            animator.SetTrigger(Idle);
            State = EnemyState.Wait;
        }

        private void Die()
        {
            State = EnemyState.Die;
            Destroy(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                State = EnemyState.PrepareToFight;
                player.GetComponent<Player>().speed = 0;
                player.GetComponent<Player>().fight = true;
                //TODO протащить сюда канвас, чтобы он отображался canvas.enabled = !canvas.enabled;
                //canvas.enabled = !canvas.enabled;
            }
                
        }
    }
}
