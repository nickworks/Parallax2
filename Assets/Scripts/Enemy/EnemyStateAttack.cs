using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyStateAttack : EnemyState
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
    /// Used to track how much time the enemy should wait before returning to Patrolling.
    /// </summary>
    float timeUntilGiveup;

    /// <summary>
    /// Acts as the "Update" Method
    /// </summary>
    public override EnemyState UpdateState()
    {
        // BEHAVIOR:
        Shoot();

        // TRANSITIONS:
        if (ShouldGiveup()) return new EnemyStatePatrol();
        
        return null;
    }
    /// <summary>
    /// This method countsdown a timer and then shoots a projectile.
    /// </summary>
    private void Shoot()
    {
        //FIXME: use delta-time (seconds) instead of frames for countdown timers
        shootTimer--; // Decrement the shootTimer 

        //If the shootTimer is less than or equal to 0, call the Shoot Method and set the shootTimer to the spawn rate.
        if (shootTimer <= 0)
        {
            controller.Shoot();
            shootTimer = spawnRate;
        }
    }

    /// <summary>
    /// Runs a timer set to a specied amount of seconds while the Enemy is in the Attack State.
    /// Once the timer reaches 0, the enemy state switches back to patrol.
    /// </summary>
    private bool ShouldGiveup()
    {
        timeUntilGiveup -= Time.deltaTime; // countdown

        if (controller.CanSeePlayer()) // if we can see the player
        {
            timeUntilGiveup = .5f; // set our giveup timer
            // TODO: make this a variable... maybe something we can set in the inspector?
        }

        if(timeUntilGiveup <= 0) return true;
        return false;
    }

}
