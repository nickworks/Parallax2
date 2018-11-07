using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using System;

public class EnemyStateAttack : State<EnemyAI>
{
    int spawnRate = 15;
    int shootTimer = 0;

    private static EnemyStateAttack _instance;

    GameObject projectile;

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
    public override void EnterState(EnemyAI _owner)
    {
        Debug.Log("Entering Attack State.");
        projectile = _owner.projectile;
    }

    public override void ExitState(EnemyAI _owner)
    {
        Debug.Log("Exiting Attack State.");
    }

    public override void UpdateState(EnemyAI _owner)
    {
        shootTimer--;
        if(shootTimer <= 0)
        {
            _owner.Shoot();
            shootTimer = spawnRate;
        }
    }
}
