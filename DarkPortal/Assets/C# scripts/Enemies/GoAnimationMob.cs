using C__scripts.Enemies;
using UnityEngine;

public class GoAnimationMob : StateMachineBehaviour
{
    private float speed;
    
    private Vector2 spawnPosition;
    private Rigidbody2D rb;
    private Transform transform;
    private bool isMoveRight;
    private float position;
    private float radius;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spawnPosition = animator.GetComponent<Enemy>().SpawnPoint.position;
        radius = animator.GetComponent<Enemy>().Radius;
        speed = animator.GetComponent<Enemy>().Speed;
        rb = animator.GetComponent<Enemy>().Rigidbody2D;
        transform = animator.GetComponent<Enemy>().Transform;
        isMoveRight = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var x = default(float);
        if (isMoveRight)
        {
            x = spawnPosition.x + radius;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            x = spawnPosition.x - radius;
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        
        var target = new Vector2(x, rb.position.y);
        var newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPos);
        if (Vector2.Distance(target, rb.position) <= 0.1f)
            isMoveRight = !isMoveRight;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform.eulerAngles = new Vector3(0, -180, 0);
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
