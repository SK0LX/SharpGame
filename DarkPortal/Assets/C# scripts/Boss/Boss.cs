using UnityEngine;

namespace C__scripts.Enemies
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] public float speed;
        [SerializeField] public GameObject canvasWIN;

        public Vector3 SpawnPosition => spawnPosition;
        
        private Animator animator;
        private Vector3 spawnPosition;
        private GameObject player;

        
        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int Go = Animator.StringToHash("go");

        public void Start()
        {
            spawnPosition = gameObject.transform.position;
            animator = gameObject.GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");
            animator.SetTrigger(Idle);
        }

        public void OnBecameVisible()
        {
            player.GetComponent<Player>().speed = 0;
            transform.eulerAngles = new Vector3(0, -180, 0);
            animator.SetTrigger(Go);
        }
    }
}
