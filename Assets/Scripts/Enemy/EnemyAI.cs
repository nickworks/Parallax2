using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class EnemyAI : MonoBehaviour
{
    public bool switchSTate = false;
    public float walkSpeed = 1;

    public Transform pointA;
    public Transform pointB;

    public GameObject enemy;

    public StateMachine<EnemyAI> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<EnemyAI>(this);
        stateMachine.ChangeState(EnemyStatePatrol.Instance);
    }
    private void Update()
    {
        stateMachine.Update();
    }

}
