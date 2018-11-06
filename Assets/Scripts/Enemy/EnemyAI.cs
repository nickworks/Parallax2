using UnityEngine;
using StateStuff;

public class EnemyAI : MonoBehaviour
{
    public float gameTimer;
    public int cooldown = 5;
    public int seconds = 0;

    [HideInInspector]
    public float walkSpeed = 1;

    [HideInInspector]
    public bool facingForward = true;

    public Transform pointA;
    public Transform pointB;
    public Transform shootPoint;

    public GameObject enemy;
    public GameObject rayCast;
    public GameObject projectile;

    [HideInInspector]
    public GameObject player;

    public StateMachine<EnemyAI> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<EnemyAI>(this);
        ChangeStateToPatrol();
        gameTimer = Time.time;
    }
    private void Update()
    {
        CastRay();
        if (stateMachine.currentState == EnemyStateAttack.Instance) AlertCooldown();
        stateMachine.Update();
    }

    private void AlertCooldown()
    {

        if (Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            seconds++;
            Debug.Log(seconds);
        }
        if (seconds == cooldown)
        {
            ChangeStateToPatrol();
            seconds = 0;
        }

    }

    public void ChangeStateToAttack()
    {
        stateMachine.ChangeState(EnemyStateAttack.Instance);
    }
    public void ChangeStateToPatrol()
    {
        stateMachine.ChangeState(EnemyStatePatrol.Instance);
    }
    public void SetTarget(GameObject o)
    {
        player = o;
    }
    public void CastRay()
    {

        RaycastHit hit;

        // The distance at which the other object is when hit.
        float theDistance;

        // Sets the forward of the Ray at a specified distance.
        Vector3 forward = rayCast.transform.TransformDirection(Vector3.right) * ((facingForward) ? 5 : -5);

        // Sets the Ray as red in the editor during play.
        Debug.DrawRay(rayCast.transform.position, forward, Color.red);

        //If the Raycast hits an object...

        if (Physics.Raycast(rayCast.transform.position, (forward), out hit, 5))
        {
            //If that object has the "Player" Tag...
            if (hit.collider.gameObject.tag == "Player")
            {
                theDistance = hit.distance;

                Debug.Log(theDistance + " " + hit.collider.gameObject.name);

                
                if (stateMachine.currentState != EnemyStateAttack.Instance) ChangeStateToAttack();
                seconds = 0;
            }
        }
    }

}
