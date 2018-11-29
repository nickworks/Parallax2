using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a surface that autojumps the player
/// </summary>
public class JumpPad : MonoBehaviour {
    /// <summary>
    /// The height the player should jump to
    /// </summary>
    private Vector3 jumpHeight;
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
                jumpHeight = new Vector3(0, 25, 0);
                playerController.velocity += jumpHeight;
            }
        }
	}
}