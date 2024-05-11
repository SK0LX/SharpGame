using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace C__scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] public int cost;
        
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
        private GameObject player;
        private Canvas canvasForFight;
        private Entity entity;
        private Fight fight;
        private SpriteRenderer spriteRenderer;
        
        private float radius;
        private float speed;
        private float boxRadius;
        private float playerBox;
        private bool isBoss;
        
        private static readonly int Go = Animator.StringToHash("go");
        private static readonly int AttackAnimation = Animator.StringToHash("attack");
        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int CriticalDamage = Animator.StringToHash("criticalDamage");
        private static readonly int Die1 = Animator.StringToHash("die");

        public void Init(GameObject gameObject, float radius, float speed, Transform spawnPosition, GameObject player, Canvas canvas, Fight fight)
        {
            this.gameObject = gameObject;
            this.radius = radius;
            this.speed = speed;
            this.spawnPosition = spawnPosition;
            this.player = player;
            this.fight = fight;
            transform = gameObject.transform;
            transform.position = spawnPosition.transform.position;
            canvasForFight = canvas;
            State = EnemyState.Born;
            animator = GetComponent<Animator>();
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            entity = gameObject.GetComponent<Entity>();
            isBoss = gameObject.GetComponent<Boss>() is not null;
            boxRadius = animator.GetComponent<BoxCollider2D>().bounds.extents.x;
            playerBox = player.GetComponent<BoxCollider2D>().bounds.extents.x;
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
                    return;
            }
        }

        public IEnumerator Attack()
        {
            //var originalColor = spriteRenderer.color;
            if (new Random().Next(0, 101) < 20)
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
            
            /*var component = GetComponent<Entity>(); 
            if (component.HasHealthDecreased())
            {
                spriteRenderer.color = Color.red; 
                yield return new WaitForSeconds(0.5f); 
            }*/
            
            animator.SetTrigger(!fight.critDamage ? AttackAnimation : CriticalDamage);
            yield return new WaitForSeconds(0.8f);
            
            animator.SetTrigger(Go);
            transform.eulerAngles = new Vector3(0, 0, 0);
            while (transform.position.x < geolocationNow)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                yield return null;
            }
            transform.eulerAngles = new Vector3(0, -180, 0);
            yield return new WaitForSeconds(0.1f);
            //spriteRenderer.color = originalColor;
            animator.SetTrigger(Idle);
        }

        public IEnumerator Die()
        {
            animator.SetTrigger(Die1);
            yield return new WaitForSeconds(0.35f);
            if (!isBoss)
                Destroy(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && State is EnemyState.Move && !isBoss)
            {
                Destroy(GetComponent<BoxCollider2D>());
                State = EnemyState.Fight;
                player.GetComponent<Player>().speed = 0;
                player.GetComponent<Player>().fight = true;
                
                fight = Instantiate(fight);
                fight.Init(player, canvasForFight, gameObject);
                entity.ShowCanvas();
                animator.ResetTrigger(Go);
                animator.SetTrigger(Idle);
                
                transform.position += new Vector3(2f, 0);
            }
            else if (other.CompareTag("Player") && State is EnemyState.Move)
            {
                StartCoroutine(PrepareToFight());
            }
        }

        private IEnumerator PrepareToFight()
        {
            player.GetComponent<Player>().speed = 0;
            yield return new WaitForSeconds(0.5f);
            animator.SetTrigger("teleport");
            yield return new WaitForSeconds(1.1f);
            transform.position += new Vector3(2f, 0);
            transform.eulerAngles = new Vector3(0, -180, 0);
            Destroy(GetComponent<BoxCollider2D>());
            State = EnemyState.Fight;
            animator.SetBool("isFight", true);
            
            player.GetComponent<Player>().fight = true;
                
            fight = Instantiate(fight);
            fight.Init(player, canvasForFight, gameObject);
            entity.ShowCanvas();
            animator.SetTrigger(Idle);
            yield return null;
        }
    }
}
