using UnityEngine;
using StateStuff;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector]
    public float walkSpeed = 1;


    public Transform pointA;
    public Transform pointB;

    public GameObject enemy;
    public GameObject rayCast;
    [HideInInspector]
    public GameObject player;

    public StateMachine<EnemyAI> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<EnemyAI>(this);
        ChangeStateToPatrol();
    }
    private void Update()
    {
        stateMachine.Update();
    }
    public void ChangeStateToAttack()
    {
        stateMachine.ChangeState(EnemyStateAttack.Instance);
    }
    public void ChangeStateToPatrol()
    {
        stateMachine.ChangeState(EnemyStatePatrol.Instance);
    }
    public void SetTarget(GameObject o)
    {
        player = o;
    }
}
