using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using System;

public class EnemyStatePatrol : State<EnemyAI>
{
    private static EnemyStatePatrol _instance;

    private EnemyStatePatrol()
    {
        if(_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static EnemyStatePatrol Instance
    {
        get
        {
            if(_instance == null)
            {
                new EnemyStatePatrol();
            }

            return _instance;

        }
    }





    public override void EnterState(EnemyAI _owner)
    {

        Debug.Log("Entering Patrol State");
        throw new NotImplementedException();
        
    }

    public override void ExitState(EnemyAI _owner)
    {

        Debug.Log("Exiting Patrol State");
        throw new NotImplementedException();
    }

    public override void UpdateState(EnemyAI _owner)
    {
        throw new NotImplementedException();
    }
}
