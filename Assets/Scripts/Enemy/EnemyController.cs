using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This handles the enemy avatar behavior (physics + AI features)
/// </summary>
[RequireComponent(typeof(LayerFixed))]
public class EnemyController : MonoBehaviour {

    /// <summary>
    /// Ref to the LayerFixed component that allows the enemy to phase jump.
    /// </summary>
    private LayerFixed layerController;
    /// <summary>
    /// Ref to the LayerFixed component on teh player
    /// </summary>
    private LayerFixed playerLayerController;
    /// <summary>
    /// Ref to a GameObject that will be the player in the scene
    /// </summary>
    private GameObject player;
    /// <summary>
    /// Ref to the Player PlayerController object that handles player behavior
    /// </summary>
    private PlayerController playerController;
    /// <summary>
    /// the player's location relative to this enemy
    /// 0 same layer 
    /// 1 in front
    /// -1 behind
    /// </summary>
    float playerDirection = 0;
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
        layerController = GetComponent<LayerFixed>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerLayerController = player.GetComponent<LayerFixed>();

    }
	
	/// <summary>
    /// The Game ticks forward one frame.
    /// </summary>
	void Update () {
        if(PlayerInSight(playerController, this))
        {
            print("player seen");
            MoveToPlayer(player.transform.position.z);
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
        float FOV = .25f;
        float playerLayerDist = DeterminRelativePlayerLocation();
        bool shouldPhase = false;


        //check how many layers away the player is located
        if (playerLayerDist == 0 || playerLayerDist < -1 || playerLayerDist > 1) {
           // print("Don't phase");
            return shouldPhase;
        }

        //check if the player is within our fov
        Vector3 forward = enemy.transform.right;//reference the vector of the enemy that is facing forward in the game window.
        Vector3 layerInFront = -enemy.transform.forward;// reference the vector that is facing towards the camera in the game window.
        Vector3 toPlayer = player.transform.position - enemy.transform.position;//subtract the player position from the enemy position.

        float distance = toPlayer.magnitude;//reference the magnitube of toPlayer to find the distance between the enemy and the player.

        float directionForward = Vector3.Dot(forward, toPlayer.normalized);// use the dot product to get a float between 1 & -1 to determine player location along the X axis in the game window.
        float directionInFront = Vector3.Dot(layerInFront, toPlayer.normalized);// using the dot product to get a float between 1 & -1 to determine the player location along the Z axis.


        if (directionForward > FOV || directionInFront > FOV)//one of the dot products is large enough so do this stuff.
        {
            //print("player seen in front");
            shouldPhase = true;
        }

        if(directionForward < FOV || directionInFront < FOV)//one of the dot products is small enough so do this stuff.
        {
            //print("player seen behind");
            shouldPhase = true;

        }

        return shouldPhase;

    }

    /// <summary>
    /// Checks the player layer, determines if the player is close enough, then asks LayerFixed to jump.
    /// </summary>
    /// <param name="playerLayer">An integer to represent what layer the player is currently on.</param>
    public void MoveToPlayer(float playerLayer)
    {
        //print(playerLayer / LayerFixed.separation);
        print(playerDirection);
        if (playerDirection == 1)// if the enemy layer is less than the player layer do this
        {
            
            print("forward");
            layerController.ComeForward();
        }

        if(playerDirection == -1)// if the enemy layer is larger than the player layer do this stuff.
        {
           // print(playerDirection);
            print("backward");
            layerController.GoBack();
        }

        layerController.CancelTimer();
        
    }

    /// <summary>
    /// this function determins whether or not the player is infront of behind or pon the same layer as this enemy
    /// </summary>
    /// <returns>the layer distance between the player and this enemy</returns>
    public float DeterminRelativePlayerLocation() {
        float playerDist = playerLayerController.z - layerController.z;

        if (playerDist == 0) {
            playerDirection = 0;
        } else if (playerLayerController.z > layerController.z) {
            playerDirection = -1;
        } else if (playerLayerController.z < layerController.z) {
            playerDirection = 1;
        } 

        return playerDist;
    }
}
