using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace C__scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] public int cost;
        [SerializeField] private float speed;
        [SerializeField] private float radius;
        
        private EnemyState State { get; set; }
        public Transform SpawnPoint => spawnPosition;
        public float Speed => speed;
        public float Radius => radius;
        public Transform Transform => transform;
        public Rigidbody2D Rigidbody2D => gameObject.GetComponent<Rigidbody2D>();
        
        
        private Animator animator;
        private new GameObject gameObject;
        private new Transform transform;
        private Transform spawnPosition;
        private Player player;
        private Canvas canvasForFight;
        private Entity entity;
        private Fight fight;
            
        private float boxRadius;
        private float playerBox;
        private bool isBoss;
        public bool isFullTimeSpawn;

        public float BoxRadius => boxRadius;
        
        private static readonly int Go = Animator.StringToHash("go");
        private static readonly int AttackAnimation = Animator.StringToHash("attack");
        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int CriticalDamage = Animator.StringToHash("criticalDamage");
        private static readonly int Die1 = Animator.StringToHash("die");

        public void Init(GameObject gameObject, Transform spawnPosition, GameObject player, Canvas canvas, Fight fight, bool isFullTimeSpawn)
        {
            this.gameObject = gameObject;
            this.spawnPosition = spawnPosition;
            this.player = player.GetComponent<Player>();
            this.fight = fight;
            transform = gameObject.transform;
            transform.position = spawnPosition.transform.position;
            canvasForFight = canvas;
            State = EnemyState.Born;
            animator = GetComponent<Animator>();
            entity = gameObject.GetComponent<Entity>();
            isBoss = gameObject.GetComponent<Boss>() is not null;
            boxRadius = animator.GetComponent<BoxCollider2D>().bounds.extents.x;
            playerBox = player.GetComponent<BoxCollider2D>().bounds.extents.x;
            this.isFullTimeSpawn = isFullTimeSpawn;
        }
        
        public void FixedUpdate()
        {
            if (gameObject is null) return;
            // review(30.06.2024): А зачем изменять состояние в цикле? Разве он не поменяется на Move и останется в этом состоянии постоянно?
            switch (State)
            {
                case EnemyState.Born:
                    State = EnemyState.Move;
                    return;
                case EnemyState.Move:
                    return;
            }
        }

        public IEnumerator Attack()
        {
            if (new Random().Next(0, 101) < 20) // review(30.06.2024): Хорошо было бы добавить extension типа Random.FlipCoin (а еще от констант избавиться)
                entity.UseSkills();
            var geolocationNow = transform.position.x;
            var geolocationPlayer = player.transform.position.x + boxRadius + playerBox;
            
            yield return new WaitForSeconds(0.3f);
            
            animator.SetTrigger(Go);
            transform.eulerAngles = new Vector3(0, -180, 0);
            while (transform.position.x  > geolocationPlayer)
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                yield return null;
            }
            
            animator.SetTrigger(!fight.critDamage ? AttackAnimation : CriticalDamage);
            yield return new WaitForSeconds(0.8f);
            
            animator.SetTrigger(Go);
            transform.eulerAngles = new Vector3(0, 0, 0);
            while (transform.position.x < geolocationNow)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                yield return null;
            }
            transform.eulerAngles = new Vector3(0, -180, 0); // review(30.06.2024): Часто встречаю эту конструкцию. Я бы выделил extension-метод, который бы описывал, зачем изменяется ротация, а то сейчас не совсем понятно
            yield return new WaitForSeconds(0.1f);
            animator.SetTrigger(Idle);
        }

        public IEnumerator Die()
        {
            animator.SetTrigger(Die1);
            yield return new WaitForSeconds(0.35f);
            if (!isBoss)
                Destroy(gameObject);
            else
            {
                yield return new WaitForSeconds(1f);
                var canvas = Instantiate(gameObject.GetComponent<Boss>().canvasWIN); // review(30.06.2024): Подозреваю, что лучше сохранить ссылку на объект, иначе он может быть собран сборщиком мусора
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && State is EnemyState.Move && !isBoss)
            {
                Destroy(GetComponent<BoxCollider2D>());
                State = EnemyState.Fight;
                player.speed = 0;
                player.fight = true;
                
                fight = Instantiate(fight);
                fight.Init(player.gameObject, canvasForFight, gameObject);
                entity.ShowCanvas();
                animator.ResetTrigger(Go);
                animator.SetTrigger(Idle);
                
                transform.position += new Vector3(2f, 0);
            }
            else if (other.CompareTag("Player") && State is EnemyState.Move) // review(30.06.2024): Условие частично дублируется, можно внутри предыдущего if сделать проверку
            {
                StartCoroutine(PrepareToFight());
            }

            // review(30.06.2024): имею в виду такое

            void Method()
            {
                if (!other.CompareTag("Player") || State is not EnemyState.Move)
                    return;
                
                if (isBoss)
                {
                    StartCoroutine(PrepareToFight());
                    return;
                }

                // review(30.06.2024): А почему это не выделили в метод, как PrepareToFight?
                Destroy(GetComponent<BoxCollider2D>());
                State = EnemyState.Fight;
                player.speed = 0;
                player.fight = true;
                
                fight = Instantiate(fight);
                fight.Init(player.gameObject, canvasForFight, gameObject);
                entity.ShowCanvas();
                animator.ResetTrigger(Go);
                animator.SetTrigger(Idle);
                
                transform.position += new Vector3(2f, 0);
            }
        }

        private IEnumerator PrepareToFight()
        {
            player.speed = 0;
            yield return new WaitForSeconds(0.5f);
            animator.SetTrigger("teleport");
            yield return new WaitForSeconds(1.1f);
            transform.position += new Vector3(2f, 0);
            transform.eulerAngles = new Vector3(0, -180, 0);
            Destroy(GetComponent<BoxCollider2D>());
            
            player.fight = true;
            State = EnemyState.Fight;
            
            animator.SetBool("isFight", true);

            // review(30.06.2024): Дублируется логика
            fight = Instantiate(fight);
            fight.Init(player.gameObject, canvasForFight, gameObject);
            player.fightObject = fight;
            entity.ShowCanvas();
            animator.SetTrigger(Idle);
        }

        public void DamagePlayer()
        {
            player.gameObject.GetComponent<Animator>().SetTrigger("GetDamage");
            player.gameObject.GetComponent<Health>().TakeHitSound();
        }
    }
}
