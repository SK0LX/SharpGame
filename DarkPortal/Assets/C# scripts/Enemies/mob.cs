using UnityEngine;

namespace C__scripts.Enemies
{
    public class Enemy
    {
        private readonly Animator animator;
        private readonly GameObject gameObject;
        private readonly Transform transform;
        private readonly Transform spawnPosition;
        
        private readonly float radius;
        private readonly float speed;

        private float position;
        private static readonly int Go = Animator.StringToHash("go");

        public EnemyState State { get; private set; }

        public Enemy(GameObject prefab, float radius, float speed, Transform spawnPosition)
        {
            gameObject = GameObject.Instantiate(prefab);
            transform = gameObject.transform;
            animator = gameObject.GetComponent<Animator>();
            State = EnemyState.None;
            this.radius = radius;
            this.speed = speed;
            this.spawnPosition = spawnPosition;
        }

        public void Born()
        {
            State = EnemyState.Born;
            transform.position = spawnPosition.position;
            gameObject.SetActive(true);
        }
        
        public void Update()
        {
            if (State == EnemyState.None) 
                return;
            if (State == EnemyState.Born)
                State = EnemyState.MoveRight;
            
            position = transform.position.x;
            var vector = Vector2.right * (speed * Time.deltaTime);
            
            if (position + vector.x < spawnPosition.position.x + radius && State is EnemyState.MoveRight)
            {
                transform.Translate(vector);
            }
            else if (position - vector.x > spawnPosition.position.x - radius)
            {
                State = EnemyState.MoveLeft;
                transform.eulerAngles = new Vector3(0, -180, 0);
                transform.Translate(vector);
            }
            else
            {
                State = EnemyState.MoveRight;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            
            animator.SetTrigger(Go);
        }
    }
}
