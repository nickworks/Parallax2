using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using System;

public class EnemyStateAttack : State<EnemyAI>
{

    public int shootTimer = 5;

    private static EnemyStateAttack _instance;

    private EnemyStateAttack()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static EnemyStateAttack Instance
    {
        get
        {
            if (_instance == null)
            {
                new EnemyStateAttack();
            }

            return _instance;
        }
    }
    void Shoot(EnemyAI _owner)
    {
        
    }
    public override void EnterState(EnemyAI _owner)
    {
        Debug.Log("Entering Attack State.");
    }

    public override void ExitState(EnemyAI _owner)
    {
        Debug.Log("Exiting Attack State.");
    }

    public override void UpdateState(EnemyAI _owner)
    {
        Shoot(_owner);
    }
}
