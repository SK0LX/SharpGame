using System.Collections;
using System.Linq;
using C__scripts.Enemies;
using UnityEngine;

// review(29.06.2024): Как будто класс не используется. Стоит удалять такое
public class GoAnimation : StateMachineBehaviour
{
    [SerializeField] public bool MoveLeft;
    
    private Transform player;
    private Vector3 spawnPoint;
    private Rigidbody2D rb;
    private float speed;
    private float boxRadius;
    private float playerBox;
    private static readonly int Idle = Animator.StringToHash("idle");
    private static readonly int Attack = Animator.StringToHash("attack");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player").transform;
        spawnPoint = animator.GetComponent<Boss>().SpawnPosition + Vector3.left * 1.5f;
        speed = animator.GetComponent<Boss>().speed;
        rb = animator.GetComponent<Rigidbody2D>();
        boxRadius = animator.GetComponent<BoxCollider2D>().bounds.extents.x;
        playerBox = player.GetComponent<BoxCollider2D>().bounds.extents.x;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (MoveLeft)
        {
            rb.transform.eulerAngles = new Vector3(0, -180, 0);
            var target = new Vector2(player.position.x, rb.position.y);
            var newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            var geolocationPlayer = player.transform.position.x + boxRadius + playerBox;
            if (animator.transform.position.x >= geolocationPlayer) return;
            MoveLeft = false;
            animator.SetTrigger(Attack);
        }
        else
        {
            rb.transform.eulerAngles = new Vector3(0, 0, 0);
            var target = new Vector2(spawnPoint.x, rb.position.y);
            var newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            if (!(Vector2.Distance(spawnPoint, rb.position) <= 2f)) return;
            MoveLeft = true;
            rb.transform.eulerAngles = new Vector3(0, -180, 0);
            animator.SetTrigger(Idle);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!MoveLeft)
            animator.ResetTrigger(Attack);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
