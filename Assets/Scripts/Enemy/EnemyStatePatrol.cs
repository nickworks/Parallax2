using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyStatePatrol : EnemyState
{
    /// <summary>
    /// A value that counts up from 0 to 1 to determine the Enemy position on its patrol lerp path.
    /// </summary>
    [HideInInspector]
    public float t = 0.0f;

    float pointA;
    float pointB;


    public override void EnterState(EnemyAI controller)
    {
        base.EnterState(controller);

        pointA = controller.pointA.transform.localPosition.x;
        pointB = controller.pointB.transform.localPosition.x;
    }

    /// <summary>
    /// Acts as the Update function. Runs the Patrol function.
    /// </summary>
    /// <param name="controller"></param>
    public override EnemyState UpdateState()
    {
        // BEHAVIOR:
        Patrol();

        // TRANSITIONS:
        if (controller.CanSeePlayer()) return new EnemyStateAttack();

        return null;
    }

    /// <summary>
    /// Lerps the Owner's billboard component between to points at a speed equal to (the owner's walk speed divided by four) per second. 
    /// When the billboard reaches point B, points A and B are reversed and the lerp amount is reset to 0.
    /// </summary>
    /// <param name="owner"></param>
    public void Patrol()
    {
        float speed = controller.walkSpeed;

        controller.enemyBody.transform.localPosition = new Vector3(Mathf.Lerp(pointA, pointB, t), controller.enemyBody.transform.localPosition.y, controller.enemyBody.transform.localPosition.z);

        t += (speed * Time.deltaTime) / (Mathf.Abs(pointB) + Mathf.Abs(pointA));

        if (t > 1) //when the lerp reaches 100%, this swaps points A and B, then resets the lerp
        {
            float tempValue = pointB;
            pointB = pointA;
            pointA = tempValue;

            t = 0;

            controller.enemyBody.transform.localEulerAngles += new Vector3(0, 180, 0); //this turns the enemy billboard so that it faces the opposite direction
        }

    }
}
