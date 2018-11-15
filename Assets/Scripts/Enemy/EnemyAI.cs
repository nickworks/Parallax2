using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    EnemyState state;

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
    /// Acts as the origin for the CastRay method.
    /// </summary>
    public GameObject rayCast;
    /// <summary>
    /// The object to be used as the enemy projectile.
    /// </summary>
    public GameObject prefabProjectile;

    public GameObject enemyBody;

    private void Start()
    {
        ChangeToState(new EnemyStatePatrol());
    }
    private void Update()
    {
        if (state != null) ChangeToState(state.UpdateState());
    }

    /// <summary>
    /// This method changes the state machine to a different state. Typically, this
    /// should be automagically called from this class's Update() method.
    /// </summary>
    /// <param name="nextState"></param>
    private void ChangeToState(EnemyState nextState)
    {
        if (nextState == null) return;
        if (state != null) state.ExitState();
        state = nextState;
        state.EnterState(this);
    }
    
    /// <summary>
    /// Shoots a projectile from a specified point.
    /// </summary>
    public void Shoot()
    {
        Instantiate(prefabProjectile, shootPoint);
    }

    /// <summary>
    /// Shoots a Ray Cast from a specified point on the Enemy.
    /// Upon hitting something, it checks to see if that thing has the "Player" tag.
    /// If the hit object has the "Player Tag" it switched the Enemy state to Attack and sets the AlertCoolDown timer to 0;
    /// </summary>
    public bool CanSeePlayer()
    {
        Vector3 forward = rayCast.transform.TransformDirection(Vector3.right); // Sets the forward of the Ray at a specified distance.
        float distance = 5;

        Debug.DrawRay(rayCast.transform.position, forward * distance, Color.red); // Draws the Ray as red in the editor during play.

        RaycastHit hit;
        if (Physics.Raycast(rayCast.transform.position, forward, out hit, distance)) // If the Raycast hits an object...
        {
            if (hit.collider.gameObject.tag == "Player") // If that object has the "Player" Tag...
            {
                return true; // can see player
            }
        }
        return false; // cannot see player
    }
}
