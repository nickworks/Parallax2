using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using System;

public class EnemyStateAttack : State<EnemyAI>
{
    /// <summary>
    /// The rate, in frames, at which projectiles are spawned
    /// </summary>
    int spawnRate = 10;
    /// <summary>
    /// The starting amount of time, in frames, before the first projectile is spawned.
    /// </summary>
    int shootTimer = 20;
    /// <summary>
    /// A usable instance of this Enemy State.
    /// </summary>
    private static EnemyStateAttack _instance;
    /// <summary>
    /// Sets the instance of this State if it hasn't been done yet.
    /// </summary>
    private EnemyStateAttack()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }
    /// <summary>
    /// Creates an instance of this state if one hasn't already been created.
    /// </summary>
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
    /// <summary>
    /// Acts as the "Start" method
    /// </summary>
    /// <param name="_owner"></param>
    public override void EnterState(EnemyAI _owner)
    {
        Debug.Log("Entering Attack State.");
    }
    /// <summary>
    /// Acts as the "Exit" Method
    /// </summary>
    /// <param name="_owner"></param>
    public override void ExitState(EnemyAI _owner)
    {
        Debug.Log("Exiting Attack State.");
    }
    /// <summary>
    /// Acts as the "Update" Method
    /// </summary>
    /// <param name="_owner"></param>
    public override void UpdateState(EnemyAI _owner)
    {
        //Decrement the shootTimer 
        shootTimer--;
        //If the shootTimer is less than or equal to 0, call the Shoot Method and set the shootTimer to the spawn rate.
        if(shootTimer <= 0)
        {
            _owner.Shoot();
            shootTimer = spawnRate;
        }
    }
}
