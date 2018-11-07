using UnityEngine;
using StateStuff;

public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// Used to track and compare the Time.time variable for the AlertCoolDown.
    /// </summary>
    float gameTimer;
    /// <summary>
    /// The amount of time, in seconds, that the enemy will wait before switching back to the patrol state.
    /// </summary>
    public int cooldown = 2;
    /// <summary>
    /// Incremental value that is used to check against the cooldown variable.
    /// </summary>
    int seconds = 0;
    /// <summary>
    /// The enemies walking speed.
    /// </summary>
    public float walkSpeed = 1;
    /// <summary>
    /// Transform position of the starting point for the Patrol lerp.
    /// </summary>
    public Transform pointA;
    /// <summary>
    /// Transform position of the ending point for the Patrol lerp.
    /// </summary>
    public Transform pointB;
    /// <summary>
    /// Transform position where projectiles are spawned.
    /// </summary>
    public Transform shootPoint;
    /// <summary>
    /// The visible component of the Sentry prefab.
    /// </summary>
    public GameObject enemy;
    /// <summary>
    /// Acts as the origin for the CastRay method.
    /// </summary>
    public GameObject rayCast;
    /// <summary>
    /// The object to be used as the enemy projectile.
    /// </summary>
    public GameObject projectile;
    /// <summary>
    /// The state machine that controls all of the state switching for EnemyAI.
    /// </summary>
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
        if (stateMachine.currentState == EnemyStateAttack.Instance) AlertCooldown(); //If the Enemy Attack State is Active, call the AlertCooldown method
        stateMachine.Update();
    }
    /// <summary>
    /// Runs a timer set to a specied amount of seconds while the Enemy is in the Attack State.
    /// Once the timer reaches 0, the enemy state switches back to patrol.
    /// </summary>
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
    /// <summary>
    /// Switch Enemy state to the active instance of the Attack State.
    /// </summary>
    public void ChangeStateToAttack()
    {
        stateMachine.ChangeState(EnemyStateAttack.Instance);
    }
    /// <summary>
    /// Switch Enemy state to the active instance of the Patrol State.
    /// </summary>
    public void ChangeStateToPatrol()
    {
        stateMachine.ChangeState(EnemyStatePatrol.Instance);
    }
    /// <summary>
    /// Shoots a Ray Cast from a specified point on the Enemy.
    /// Upon hitting something, it checks to see if that thing has the "Player" tag.
    /// If the hit object has the "Player Tag" it switched the Enemy state to Attack and sets the AlertCoolDown timer to 0;
    /// </summary>
    public void CastRay()
    {

        RaycastHit hit;

        // The distance at which the other object is when hit.
        float theDistance;

        // Sets the forward of the Ray at a specified distance.
        Vector3 forward = rayCast.transform.TransformDirection(Vector3.right) * 5;

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

                
                if (stateMachine.currentState != EnemyStateAttack.Instance) ChangeStateToAttack(); //Only call the method if the Enemy State isn't already Attack.
                seconds = 0;
            }
        }
    }
    /// <summary>
    /// Shoots a projectile from a specified point.
    /// </summary>
    public void Shoot()
    {
        Instantiate(projectile, shootPoint);
    }
}
