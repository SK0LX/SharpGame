using System.Collections;
using UnityEngine;

namespace C__scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private Animator animator;
        private new GameObject gameObject;
        private new Transform transform;
        private Transform spawnPosition;
        private GameObject player;
        private Canvas canvasForFight;
        private Entity entity;
        private Fight fight;
        
        private float radius;
        private float speed;

        private float position;
        private bool isMoveRight = true;
        private static readonly int Go = Animator.StringToHash("go");
        private static readonly int AttackAnimation = Animator.StringToHash("attack");
        private static readonly int Idle = Animator.StringToHash("idle");

        public EnemyState State { get; private set; }

        public void Init(GameObject gameObject, float radius, float speed, Transform spawnPosition, GameObject player, Canvas canvas, Fight fight)
        {
            this.gameObject = gameObject;
            position = spawnPosition.position.x;
            animator = gameObject.GetComponent<Animator>();
            this.radius = radius;
            this.speed = speed;
            this.spawnPosition = spawnPosition;
            this.player = player;
            transform = gameObject.transform;
            transform.position = spawnPosition.transform.position;
            State = EnemyState.Born;
            canvasForFight = canvas;
            this.fight = fight;
            entity = gameObject.GetComponent<Entity>();
        }
        
        public void FixedUpdate()
        {
            if (gameObject is null) return;
            switch (State)
            {
                case EnemyState.Born:
                    State = EnemyState.Move;
                    return;
                case EnemyState.Move:
                    Move();
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

        public IEnumerator Attack()
        {
            var geolocationNow = transform.position.x;
            var geolocationPlayer = player.transform.position.x;
            
            yield return new WaitForSeconds(1f);
            
            animator.SetTrigger(Go);
            transform.eulerAngles = new Vector3(0, -180, 0);
            while (transform.position.x > geolocationPlayer + 2)
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                yield return null;
            }
            
            animator.SetTrigger(AttackAnimation);
            yield return new WaitForSeconds(0.6f);
            
            animator.SetTrigger(Go);
            transform.eulerAngles = new Vector3(0, 0, 0);
            while (transform.position.x < geolocationNow)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                yield return null;
            }
            transform.eulerAngles = new Vector3(0, -180, 0);
            
            animator.SetTrigger(Idle);
        }

        public IEnumerator Die()
        {
            animator.SetTrigger("die");
            yield return new WaitForSeconds(0.35f);
            Destroy(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && State is EnemyState.Move)
            {
                transform.position += new Vector3(1f, 0);
                transform.eulerAngles = new Vector3(0, -180, 0);
                State = EnemyState.Fight;
                player.GetComponent<Player>().speed = 0;
                player.GetComponent<Player>().fight = true;
                Instantiate(fight).Init(player, canvasForFight, gameObject);
                entity.ShowCanvas();
            }
        }
    }
}
