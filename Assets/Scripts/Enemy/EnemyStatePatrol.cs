using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using System;

public class EnemyStatePatrol : State<EnemyAI>
{
    private static EnemyStatePatrol _instance;

    public float t = 0.0f;
    
    

    private EnemyStatePatrol()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static EnemyStatePatrol Instance
    {
        get
        {
            if (_instance == null)
            {
                new EnemyStatePatrol();
            }

            return _instance;

        }
    }

    public void Patrol(EnemyAI _owner)
    {
        float pointA = _owner.pointA.transform.localPosition.x;
        float pointB = _owner.pointB.transform.localPosition.x;

        float speed = _owner.walkSpeed / 2;

    _owner.enemy.transform.localPosition = new Vector3(Mathf.Lerp(pointA, pointB, t), 0, 0);

        t += speed * Time.deltaTime;

        if (t > 1 || t < 0) speed *= -1;


    }

    public override void EnterState(EnemyAI _owner)
    {
        Debug.Log("Entering Patrol State");
    }

    public override void ExitState(EnemyAI _owner)
    {
        Debug.Log("Exiting Patrol State");
    }

    public override void UpdateState(EnemyAI _owner)
    {
        Patrol(_owner);
    }
}
