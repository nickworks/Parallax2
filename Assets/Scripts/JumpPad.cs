using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a surface that autojumps the player
/// </summary>
public class JumpPad : MonoBehaviour {
    /// <summary>
    /// The velocity to use when bouncing the player
    /// </summary>
    public Vector3 jumpVelocity = new Vector3(0, 35, 0);
    /// <summary>
    /// Reference to the player controller
    /// </summary>
    private PlayerController playerController;
    /// <summary>
    /// A reference to the play game object
    /// </summary>
    private GameObject player;
/// <summary>
/// Grabs references to the player game object and the player controller script
/// </summary>
    private void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    /// <summary>
    /// When collision happens, and the tag is player, and if the collision volume is a trigger, the player auto jumps
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other){
        print(playerController);
        if (other.gameObject.tag == "Player")
        {
            if (other.isTrigger == true)
            {
                playerController.velocity += jumpVelocity;
            }
        }
	}
}