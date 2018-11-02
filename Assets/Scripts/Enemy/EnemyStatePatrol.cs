using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using System;

public class EnemyStatePatrol : State<EnemyAI>
{
    private static EnemyStatePatrol _instance;

    bool facingForward = true;

    public Transform pointA;
    public Transform pointB;

    public GameObject hitObject;

    public GameObject gameObject;

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

        

        float speed = _owner.walkSpeed / 4;

        _owner.enemy.transform.localPosition = new Vector3(Mathf.Lerp(pointA.transform.localPosition.x, pointB.transform.localPosition.x, t), 0, 0);

        t += speed * Time.deltaTime;


        if (t > 1)
        {
            Transform tempValue = pointB;
            pointB = pointA;
            pointA = tempValue;

            t = 0;

            facingForward = !facingForward;
        }

    }

    public override void EnterState(EnemyAI _owner)
    {
        Debug.Log("Entering Patrol State");
        pointA = _owner.pointA;
        pointB = _owner.pointB;
        gameObject = _owner.rayCast;
    }

    public override void ExitState(EnemyAI _owner)
    {
        Debug.Log("Exiting Patrol State");
    }

    public override void UpdateState(EnemyAI _owner)
    {
        Patrol(_owner);
        CastRay(_owner);
    }
    public void CastRay(EnemyAI _owner)
    {

        RaycastHit hit;


        // The distance at which the other object is when hit.
        float theDistance;

        // Sets the forward of the Ray at a specified distance.
        Vector3 forward = gameObject.transform.TransformDirection(Vector3.right) * ((facingForward) ? 5:-5);

        // Sets the Ray as red in the editor during play.
        Debug.DrawRay(gameObject.transform.position, forward, Color.red);

        //If the Raycast hits an object...
        if (Physics.Raycast(gameObject.transform.position, (forward), out hit, 10))
        {
            //If that object has the "Interactable" Tag...
            if (hit.collider.gameObject.tag == "Player")
            {
                theDistance = hit.distance;

                Debug.Log(theDistance + " " + hit.collider.gameObject.name);

                _owner.ChangeStateToAttack();
            }
        }
    }
}
