using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This handles the enemy avatar behavior (physics + AI features)
/// </summary>
[RequireComponent(typeof(LayerFixed))]
public class EnemyController : MonoBehaviour {

    /// <summary>
    /// Ref to the LayerFixed object that allows the enemy to phase jump.
    /// </summary>
    private LayerFixed layerController;
    /// <summary>
    /// Ref to a GameObject that will be the player in the scene
    /// </summary>
    private GameObject player;
    /// <summary>
    /// Ref to the Player PlayerController object that handles player behavior
    /// </summary>
    private PlayerController playerController;
    /// <summary>
    /// A boolean that is used to identify that the player is within a certain distance from the enemy 
    /// </summary>
    private bool playerIsSpotted = false;
    /// <summary>
    /// A boolean that is used to identify that the player is close enough for the enemy to phase jump to the player layer.
    /// </summary>
    private bool playerIsCloseEnough = false;
    /// <summary>
    /// Initializes the object. Called when spawning.
    /// </summary>
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        playerController = player.GetComponent<PlayerController>();
	}
	
	/// <summary>
    /// The Game ticks forward one frame.
    /// </summary>
	void Update () {
        if(PlayerInSight(playerController, this))
        {
            MoveToPlayer(player.transform.z);
        }
	}
    /// <summary>
    /// Checks to find the distance from the enemy to the player.
    /// </summary>
    /// <param name="player">The PlayerController attached to the player. USed to find the player location</param>
    /// <param name="enemy">The EnemyController object attached to each enemy. Used to find an enemies location.</param>
    /// <returns></returns>
    public bool PlayerInSight(PlayerController player, EnemyController enemy)
    {
        Vector3 forward = enemy.transform.right;//reference the vector of the enemy that is facing forward in the game window.
        Vector3 layerInFront = -enemy.transform.forward;// reference the vector that is facing towards the camera in the game window.
        Vector3 toPlayer = player.transform.position - enemy.transform.position;//subtract the player position from the enemy position.

        float distance = toPlayer.magnitude;//reference the magnitube of toPlayer to find the distance between the enemy and the player.

        float directionForward = Vector3.Dot(forward, toPlayer.normalized);// use the dot product to get a float between 1 & -1 to determine player location along the X axis in the game window.
        float directionInFront = Vector3.Dot(layerInFront, toPlayer.normalized);// using the dot product to get a float between 1 & -1 to determine the player location along the Z axis.

        if(directionForward > .25f || directionInFront > .25f)//one of the dot products is large enough so do this stuff.
        {
            playerIsSpotted = true;
        }

        if(directionForward < .25f || directionInFront < .25f)//one of the dot products is small enough so do this stuff.
        {
            playerIsSpotted = false;
            
        }

        if(directionForward >= .5f || directionInFront >= .5f)//one of the dot products is large enough so do this stuff.
        {
            if(PlayerInSight) playerIsCloseEnough = true;
        }

        if (directionForward >= .5f || directionInFront >= .5f)//one of the dot products is small enough so do this stuff.
        {
            if (PlayerInSight) playerIsCloseEnough = false;
        }

        if (distance <= 100)// if distance is smaller than a set number do this stuff.
        {
            if(playerIsCloseEnough)// if the player is close enough do this stuff.
            {
                RaycastHit hit;// send out a raycast.
                if(Physics.Raycast(enemy.transform.position, toPlayer, out hit, Mathf.Infinity)) //if the raycast meets the requirements return true.
                {
                    return true;//the player is close enough so return true.
                }
            }
        }

        return false; // if none of the requirements are met the the player is not in sight so return flase.
    }
    /// <summary>
    /// Checks the player layer, determines if the player is close enough, then asks LayerFixed to jump.
    /// </summary>
    /// <param name="playerLayer">An integer to represent what layer the player is currently on.</param>
    public void MoveToPlayer(int playerLayer)
    {
        if(this.layerController.z < playerLayer)// if the enemy layer is less than the player layer do this
        {
            layerController.ComeForward();
        }

        if(this.layerController.z > playerLayer)// if the enemy layer is larger than the player layer do this stuff.
        {
            layerController.GoBack();
        }

        if(this.layerController.z == playerLayer)// if the enemy layer is equal to the player layer do this stuff.
        {
            layerController.CancelTimer();
        }
    }
}
