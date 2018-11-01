using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector]
    public bool switchState = false;
    public float walkSpeed = 1;


    public Transform pointA;
    public Transform pointB;

    public GameObject enemy;
    public GameObject rayCast;

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
    private void ChangeStateToAttack()
    {
        stateMachine.ChangeState(EnemyStateAttack.Instance);
    }
    private void ChangeStateToPatrol()
    {
        stateMachine.ChangeState(EnemyStatePatrol.Instance);
    }
}
