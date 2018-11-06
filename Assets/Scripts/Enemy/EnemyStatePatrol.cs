using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using System;

public class EnemyStatePatrol : State<EnemyAI>
{
    /// <summary>
    /// Creates an instance of the Enemy Patrol State for use in the Enemy AI State Machine.
    /// </summary>
    private static EnemyStatePatrol _instance;
    /// <summary>
    /// Creates the start point for the Patrol route.
    /// </summary>
    public Transform pointA;
    /// <summary>
    /// Creates the end point for the Patrol route.
    /// </summary>
    public Transform pointB;
    /// <summary>
    /// A value that counts up from 0 to 1 to determine the Enemy position on its patrol lerp path.
    /// </summary>
    [HideInInspector]
    public float t = 0.0f;
    /// <summary>
    /// Sets the EnemyStatePatrol instance to this preexisting one if one has not already been set.
    /// </summary>
    private EnemyStatePatrol()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }
    /// <summary>
    /// Creates a new instance of EnemyStatePatrol.
    /// </summary>
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
    /// <summary>
    /// Lerps the Owner's billboard component between to points at a speed equal to (the owner's walk speed divided by four) per second. 
    /// When the billboard reaches point B, points A and B are reversed and the lerp amount is reset to 0.
    /// </summary>
    /// <param name="_owner"></param>
    public void Patrol(EnemyAI _owner)
    {
        float speed = _owner.walkSpeed / 4;

        _owner.enemy.transform.localPosition = new Vector3(Mathf.Lerp(pointA.transform.localPosition.x, pointB.transform.localPosition.x, t), 0, 0);

        t += speed * Time.deltaTime;


        if (t > 1)
        {
            Transform tempValue = pointB;
            pointB = pointA;
            pointA = tempValue;

            t = 0;

            _owner.facingForward = !_owner.facingForward;
        }

    }
    /// <summary>
    /// Acts as the "Start" function. Sets points A and B and sets the owner's facing direction to forward.
    /// </summary>
    /// <param name="_owner"></param>
    public override void EnterState(EnemyAI _owner)
    {
        Debug.Log("Entering Patrol State");
        pointA = _owner.pointA;
        pointB = _owner.pointB;
        _owner.facingForward = true;
    }
    /// <summary>
    /// Code to be called upon exiting this state.
    /// </summary>
    /// <param name="_owner"></param>
    public override void ExitState(EnemyAI _owner)
    {
        Debug.Log("Exiting Patrol State");
    }
    /// <summary>
    /// Acts as the Update function. Runs the Patrol function.
    /// </summary>
    /// <param name="_owner"></param>
    public override void UpdateState(EnemyAI _owner)
    {
        Patrol(_owner);
    }
    
}
