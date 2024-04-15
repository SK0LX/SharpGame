using UnityEngine;
using UnityEngine.AI;

namespace C__scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class mob : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private float distance;

        public NavMeshAgent myAgent;
        public Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            myAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > 10)
            {
                myAgent.enabled = false;
                animator.SetBool("Idle", true);
                animator.SetBool("Run", false);
                animator.SetBool("Attack", false);
            }
            if (distance <= 10 & distance > 1.5f)
            {
                myAgent.enabled = true;
                myAgent.SetDestination(target.transform.position);
                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);
                animator.SetBool("Attack", false);
            }
            if (distance <= 1.5f)
            {
                myAgent.enabled = false;
                animator.SetBool("Idle", false);
                animator.SetBool("Run", false);
                animator.SetBool("Attack", true);
            }
            
        }
    }
}
